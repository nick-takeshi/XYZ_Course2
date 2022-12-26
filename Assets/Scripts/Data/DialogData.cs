using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogData
{
    //[SerializeField] private string[] _sentences;
    //public string[] Sentences => _sentences;

    [SerializeField] private Sentence[] _sentences;
    [SerializeField] private DialogType _type;

    public Sentence[] Sentences => _sentences;
    public DialogType Type => _type;
}

[Serializable]
public struct Sentence
{
    [SerializeField] private string _valued;
    [SerializeField] public Sprite _icon;
    [SerializeField] Side _side;

    public string Valued => _valued;
    public Side Side => _side;
}

public enum Side
{
    Left,
    Right
}

public enum DialogType
{
    Simple,
    Personalized
}
