using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoundLibrary : MonoBehaviour
{

    List <AudioClip> sounds;
    [SerializeField] Button buttonPrefab;
    AudioSource source;


    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        source = GetComponent<AudioSource>();
        sounds = new List<AudioClip>();
        Object[] objects = Resources.LoadAll("Audio/");
        foreach(Object _object in objects)
        {
            if(_object is AudioClip)
            {
                AudioClip clip = (AudioClip)_object;
                sounds.Add(clip);
            }
        }
        StartCoroutine(CreateButtons());
    }

    IEnumerator CreateButtons()
    {
        foreach(AudioClip clip in sounds)
        {
            Button _button = Instantiate(buttonPrefab, transform);
            _button.onClick.AddListener(() => source.PlayOneShot(clip));
            _button.GetComponentInChildren<TextMeshProUGUI>().text = clip.name;

            UpdateRectSize();
            yield return new WaitForEndOfFrame();
        }
        
    }

    public void AddClip(AudioClip clip, string clipName)
    {
        Button _button = Instantiate(buttonPrefab, transform);
        _button.onClick.AddListener(() => source.PlayOneShot(clip));
        _button.GetComponentInChildren<TextMeshProUGUI>().text = clipName;
        UpdateRectSize();
    }

    void UpdateRectSize()
    {

        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, transform.childCount/7 * 110);
    }
}
