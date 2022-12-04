using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] public UnityEvent _onDie;
    [SerializeField] private UnityEvent _onHealing;
    [SerializeField] public HealthChangeEvent _onChange;


    public int Health => _health;

    public void ApplyDamage(int damageValue)
    {
        if (_health <= 0) return;

        _health -= damageValue;

        _onDamage?.Invoke();

        _onChange?.Invoke(_health);

        if (_health <= 0)
        {
            _onDie?.Invoke();
        }

        
    }
    public void HealHP(int healValue)
    {
        _health += healValue;

    }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {

    }
}
