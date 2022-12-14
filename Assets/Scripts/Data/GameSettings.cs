using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundSetting
{
    Music,
    Sfx
}

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private FloatPersistanceProperty _music;
    [SerializeField] private FloatPersistanceProperty _sfx;

    public FloatPersistanceProperty Music => _music;
    public FloatPersistanceProperty Sfx => _sfx;

    private static GameSettings _instance;
    public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

    private static GameSettings LoadGameSettings()
    {
        return _instance = Resources.Load<GameSettings>("GameSettings");
    }

    private void OnEnable()
    {
        _music = new FloatPersistanceProperty(1, SoundSetting.Music.ToString());
        _sfx = new FloatPersistanceProperty(1, SoundSetting.Sfx.ToString());
    }

    private void OnValidate()
    {
        Music.Validate();
        Sfx.Validate();
    }
}


