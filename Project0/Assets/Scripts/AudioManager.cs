using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSourceMain;
    [SerializeField] private AudioSource audioSourceVFX;
   public void HelloEcho()
    {
        AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");

        audioSourceMain.outputAudioMixerGroup = audioMixGroup[0];
        audioSourceVFX.outputAudioMixerGroup = audioMixGroup[0];

    }

    public void ByeEcho()
    {
        AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");

        audioSourceMain.outputAudioMixerGroup = null;
        audioSourceVFX.outputAudioMixerGroup = null;
    }
}
