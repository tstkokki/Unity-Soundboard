using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ReadFromFile : MonoBehaviour
{

    [SerializeField] SoundLibrary library;
    AudioType currentType;
    public void CreateAudioClipFromFile(string _path)
    {
        StartCoroutine(GetAudioClip(_path));
    }

    IEnumerator GetAudioClip(string _path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            if(www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("error");
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                string _clipName = _path.Substring(_path.LastIndexOf('\\')+1);
                library.AddClip(clip, _clipName);
            }
        }
    }
}
