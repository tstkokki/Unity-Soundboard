using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeChange : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    public void ChangeVolume(float value)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(value) * 30);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);

    }

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("Volume", 0.3f);
        ChangeVolume(slider.value);
    }
}
