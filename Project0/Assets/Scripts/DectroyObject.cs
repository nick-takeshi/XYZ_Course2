using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectroyObject : MonoBehaviour
{
    [SerializeField] private GameObject _objToDestroy;
    [SerializeField] private Animator _animator;
    private static readonly int pickUpKey = Animator.StringToHash("pickup");
    private static readonly int pickUpPotion = Animator.StringToHash("pickUp");
    public void DestroyObject()
    {
        Destroy(_objToDestroy, 0.2f);
        _animator.SetTrigger(pickUpKey);
    }
    public void DestroyPotion()
    {
        Destroy(_objToDestroy, 0.5f);
        _animator.SetTrigger(pickUpPotion);
    }

    public void DestroyParticle()
    {
        Destroy(_objToDestroy);
    }

    public void DestroyBarrel()
    {
        Destroy(_objToDestroy, 0.1f);
    }
}
