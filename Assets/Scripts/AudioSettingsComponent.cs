using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSettingsComponent : MonoBehaviour
{
    [SerializeField] private SoundSetting _mode;
    private FloatPersistanceProperty _model;
    private AudioSource _source;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        _model = FindProperty();
        _model.OnChanged += OnSoundSettingChanged;
        OnSoundSettingChanged(_model.Value, _model.Value);
    }

    private void OnSoundSettingChanged(float newValue, float oldValue)
    {
        _source.volume = newValue;
    }
    private FloatPersistanceProperty FindProperty()
    {
        switch (_mode)
        {
            case SoundSetting.Music:
                return GameSettings.I.Music;

            case SoundSetting.Sfx:
                return GameSettings.I.Sfx;
        }

        throw new ArgumentException("Undefined mode");
    }

}
