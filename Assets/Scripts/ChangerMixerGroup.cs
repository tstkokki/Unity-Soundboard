using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class ChangerMixerGroup : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource sourceAction;
    [SerializeField] AudioSource sourceCreature;
    [SerializeField] AudioSource loopSource;
    [SerializeField] AudioMixerGroup group;

    public void SwitchGroup()
    {
        source.outputAudioMixerGroup = group;
        sourceAction.outputAudioMixerGroup = group;
        sourceCreature.outputAudioMixerGroup = group;
        loopSource.outputAudioMixerGroup = group;
    }
}
