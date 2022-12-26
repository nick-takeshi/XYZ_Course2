using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _state;
    [SerializeField] private string _animationKey;
    [SerializeField] private bool _updateonStart;

    private void Start()
    {
        if (_updateonStart)
            _animator.SetBool(_animationKey, _state);
    }
    public void Switch()
    {
        _state = !_state;
        _animator.SetBool(_animationKey, _state);
    }

    [ContextMenu("Switch")]

    public void SwitchIt()
    {
        Switch();
    }
}
