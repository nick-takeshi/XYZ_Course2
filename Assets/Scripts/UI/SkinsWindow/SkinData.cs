using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkinData 
{
    [SerializeField] private StringProperty _used = new StringProperty();
    [SerializeField] private AnimatorProperty _anim = new AnimatorProperty();
    [SerializeField] private List<string> _unlocked;
    public StringProperty Used => _used;
    public AnimatorProperty Anim => _anim;
    public void AddSkin(string id)
    {
        if (!_unlocked.Contains(id))
        {
            _unlocked.Add(id);
        }
    }

    public bool IsUnlocked(string id)
    {
        return _unlocked.Contains(id);
    }

}
