using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class ChangerMixerGroup : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioMixerGroup group;

    public void SwitchGroup()
    {
        source.outputAudioMixerGroup = group;
    }
}
