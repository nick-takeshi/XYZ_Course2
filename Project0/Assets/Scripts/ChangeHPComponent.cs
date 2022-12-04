using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHPComponent : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _healing;
    
    public void ApplyDamage(GameObject target)
    {
        
        var healthComponent = target.GetComponent<HealthComponent>();


        if (healthComponent != null)
        {
            if (target.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>()._session.Data.Hp.Value -= _damage;
                healthComponent.ApplyDamage(_damage);
            }
            else healthComponent.ApplyDamage(_damage);

        }
       
    }

    public void ApplyHealing(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();


        if (healthComponent != null)
        {
            if (target.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>()._session.Data.Hp.Value += _healing;
                healthComponent.HealHP(_healing);
            }
            else healthComponent.HealHP(_healing);
        }
    }
}
