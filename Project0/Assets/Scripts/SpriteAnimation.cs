using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int _frameRate;
    [SerializeField] private bool _loop; 
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private UnityEvent _onComplete;

    private SpriteRenderer _renderer;
    private float _secondsPerFrame;
    private int _currentSprite;
    private float _nextFrameTime;
    private bool _isPlaying = true;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time + _secondsPerFrame;

    }

    private void Update()
    {
        if (!_isPlaying || _nextFrameTime > Time.time) return;

        if (_currentSprite >= _sprites.Length)
        {
            if (_loop)
            {
                _currentSprite = 0;
            }
            else
            {
                _isPlaying = false;
                _onComplete.Invoke();
                return;
            }
        }

        _renderer.sprite = _sprites[_currentSprite];
        _nextFrameTime += _secondsPerFrame;
        _currentSprite++; ;
    }

    

    
}
