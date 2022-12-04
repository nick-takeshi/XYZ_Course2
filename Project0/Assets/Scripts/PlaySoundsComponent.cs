using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsComponent : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;

    public void Start()
    {
        _source = GameObject.FindGameObjectWithTag("SfxAudioSource").GetComponent<AudioSource>();
    }
    public void Play(string id)
    {
        foreach (var audioData in _sounds)
        {
            if (audioData.ID != id) continue;

            if (_source == null)
            {
                _source = GameObject.FindWithTag("SFXAudioSource").GetComponent<AudioSource>();
            }

            _source.PlayOneShot(audioData.Clip);
            break;
        }
    }

    [Serializable]

    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string ID => _id;
        public AudioClip Clip => _clip;
    }
}
