using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class ReadFromFile : MonoBehaviour
{
    TMP_InputField inputField;
    [SerializeField] SoundLibrary library;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
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
                library.AddClip(clip, _clipName);
                inputField.text = "";
            }
        }
    }
}
