using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPointer : MonoBehaviour
{
    [SerializeField] Sprite[] _keys;
    [SerializeField] Sprite _pointer;
    public void ShowHelp()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = true;
    }

    public void HideHelp()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void SetHelp()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _pointer;
    }

    public void RequireKey(int NumKey)
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _keys[NumKey];
        _spriteRenderer.enabled = true;
        Invoke("SetHelp", 1);


    }
}
