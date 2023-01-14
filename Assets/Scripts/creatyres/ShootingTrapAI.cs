using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrapAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;

    [Header("Melee")]
    [SerializeField] private Cooldown _meleeDelay;
    [SerializeField] private CheckCircleOverlap _meleeAttack;
    [SerializeField] private LayerCheck _meleeCanAttack;

    [Header("Range")]
    [SerializeField] private Cooldown _rangeDelay;
    [SerializeField] private SpawnComponent _rangeAttack;

    private Animator _animator;
    private GameObject _getPearl;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _getPearl = GameObject.Find("GetPearles");
        _getPearl.SetActive(false);

    }
    private void Update()
    {
        if (_vision.IsTouchingLayer)
        {
            if (_rangeDelay.IsReady)
            {
                RangeAttack();
            }

            if (_meleeCanAttack.IsTouchingLayer)
            {
                if (_meleeDelay.IsReady)
                {
                    MeleeAttack(); return;
                }
            }
            
            
        }
    }
    private void MeleeAttack()
    {
        _meleeDelay.Reset();
        _animator.SetTrigger("melee");
    }
    private void RangeAttack()
    {
        _rangeDelay.Reset();
        _animator.SetTrigger("range");
    }

    public void OnMeleeAttack()
    {
        _meleeAttack.Check();
    }

    public void OnRangeAttack()
    {
        _rangeAttack.Spawn();
    }

    public void OnDie()
    {
        gameObject.layer = 7;
        _getPearl.SetActive(true);
        _getPearl.layer = 0;

        Destroy(GetComponent<ShootingTrapAI>());
    }
}
