using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;
public class SoundLibrary : MonoBehaviour
{

    List<AudioClip> sounds;
    [SerializeField] Button buttonPrefab;
    public string folderName = "Audio/";
    AudioSource source;
    [SerializeField] AudioSource loopSource;
    RectTransform rectTransform;

    [SerializeField] ParticleSystem top;
    [SerializeField] ParticleSystem bottom;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
        rectTransform = GetComponent<RectTransform>();
        source = GetComponent<AudioSource>();
        sounds = new List<AudioClip>();
        Object[] objects = Resources.LoadAll(folderName);
        foreach (Object _object in objects)
        {
            if (_object is AudioClip)
            {
                AudioClip clip = (AudioClip)_object;
                sounds.Add(clip);
            }
        }


        StartCoroutine(CreateButtons());


    }

    int count = 0;
    IEnumerator CreateButtons()
    {
        foreach (AudioClip clip in sounds)
        {
            AddClip(clip, clip.name);
            UpdateRectSize();
            yield return new WaitForEndOfFrame();
        }
        var fileinfo = new DirectoryInfo(Application.persistentDataPath + "/Sounds/" + folderName).GetFiles();
        foreach (FileInfo f in fileinfo)
        {
            if (f.Extension == ".mp3" || f.Extension == ".ogg" || f.Extension == ".wav")
            {
                CreateAudioClipFromFile(f.FullName);

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void AddClip(AudioClip clip, string clipName)
    {
        Button _button = Instantiate(buttonPrefab, transform);
        _button.onClick.AddListener(() => Play(clip));
        _button.GetComponentInChildren<TextMeshProUGUI>().text = clipName;
        _button.GetComponentsInChildren<TextMeshProUGUI>()[1].text = count.ToString();
        count++;
        UpdateRectSize();
    }

    void Play(AudioClip clip)
    {
        if (loopSource != null && !loopSource.isPlaying)
            loopSource.clip = clip;

        source.PlayOneShot(clip);
        Emit(clip.length);
    }
    bool topOrBottom = false;
    void Emit(float length = 0.3f)
    {
        var main = (topOrBottom) ? top.main : bottom.main;
        main.startLifetime = length;
        if (topOrBottom)
            top.Play();
        else
            bottom.Play();
        topOrBottom = !topOrBottom;
    }


    public void PlayInput(int _input)
    {
        if (_input < sounds.Count)
        {
            source.PlayOneShot(sounds[_input]);
            Emit();
        }
    }

    void UpdateRectSize()
    {

        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, transform.childCount / 7 * 110);
    }


    public void CreateAudioClipFromFile(string _path)
    {
        if (_path.Length > 5)
        {


            _path = _path.Replace("\"", "");
            string fileType = _path.Substring(_path.Length - 4);
            fileType = fileType.ToLower();
            switch (fileType)
            {
                case ".wav":
                    StartCoroutine(GetAudioClip(_path, AudioType.WAV));
                    break;
                case ".mp3":
                    StartCoroutine(GetAudioClip(_path));
                    break;
                case ".ogg":
                    StartCoroutine(GetAudioClip(_path, AudioType.OGGVORBIS));
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator GetAudioClip(string _path, AudioType _audioType = AudioType.MPEG)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_path, _audioType))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("error");
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                string _clipName = _path.Substring(_path.LastIndexOf('\\') + 1);
                AddClip(clip, _clipName);
            }
        }
    }
}
