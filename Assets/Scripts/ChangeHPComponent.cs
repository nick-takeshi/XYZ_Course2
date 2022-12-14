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
            if (transform.parent.gameObject.name == "Hero(Clone)")
            {
                var _session = FindObjectOfType<GameSession>(); 
                var _hero = FindObjectOfType<Hero>();
                var damageValue = (int)_session.StatsModel.GetValue(StatId.RangeDamage);
                damageValue = _hero.ModifyDamageByCrit(damageValue);
                SetDamage(damageValue);

            }
            healthComponent.ApplyDamage(_damage);
            
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

    public void SetDamage(int delta)
    {
        _damage = delta;
    }
}
