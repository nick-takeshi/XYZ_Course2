using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WO_M_ShootingTrap : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;

    [Header("Range")]
    [SerializeField] private Cooldown _rangeDelay;
    [SerializeField] private SpawnComponent _rangeAttack;
    

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_vision.IsTouchingLayer)
        {
            if (_rangeDelay.IsReady)
            {
                RangeAttack();
            }
        }
    }
    private void RangeAttack()
    {
        _rangeDelay.Reset();
        _animator.SetTrigger("attack");
    }
    public void OnRangeAttack()
    {
        _rangeAttack.Spawn();
    }

    public void OnDie()
    {
        gameObject.layer = 7;

        var script = FindObjectOfType<ShootingTrapAI>();
        Destroy(script);

        var im = GetComponent<SpriteRenderer>();
        im.sortingLayerID = 0;
    }
}
