using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterApplyDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _damageDelay;

    private bool _isDry;
    private Coroutine _coroutine;

    public void ApplyDamage()
    {

        _isDry = true;
        _coroutine = StartCoroutine(Damage());

    }

    public IEnumerator Damage()
    {
        var _hero = FindObjectOfType<Hero>();

        while (_isDry)
        {

            var healthComponent = _hero.GetComponent<HealthComponent>();
            healthComponent.ApplyDamage(_damage);

            yield return new WaitForSeconds(_damageDelay);

        }
    }

    public void EndDamage()
    {
        _coroutine = null;
        _isDry = false;

    }


}
