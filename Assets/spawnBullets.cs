using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;


public class spawnBullets : MonoBehaviour
{
    [SerializeField] private UnityEvent[] _spawners;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;


    public void StartSpawn()
    {
        _coroutine = StartCoroutine(Spawn());
    }
    public IEnumerator Spawn()
    {
        for (int i = 0; i < _spawners.Length - 1; i++)
        {
            if (i > _spawners.Length)
            {
                EndSpawn();
            }
            else
            {
                _spawners[i].Invoke();
                yield return new WaitForSeconds(_delay);
            }
        }
    }

   public void EndSpawn()
    {
        _coroutine = null;
    }
}
