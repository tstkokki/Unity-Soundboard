using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class SoundLibrary : MonoBehaviour
{

    List<AudioClip> sounds;
    [SerializeField] Button buttonPrefab;
    public string folderName = "Audio/";
    AudioSource source;
    [SerializeField] AudioSource loopSource;
    RectTransform rectTransform;

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
    }


    public void PlayInput(int _input)
    {
        if (_input < sounds.Count) source.PlayOneShot(sounds[_input]);
    }

    void UpdateRectSize()
    {

        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, transform.childCount / 7 * 110);
    }
}
