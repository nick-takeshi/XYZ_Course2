using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawnClouds : MonoBehaviour
{
    [SerializeField] private GameObject _cloud;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private bool _randomDelay;
    [SerializeField] private float _secondSpawnDelay;

    private Coroutine _coroutine;


    public void Start()
    {
        if (_randomDelay)
        {
           _coroutine = StartCoroutine(RandomSpawn());
        }
        else
        {
            _coroutine = StartCoroutine(Spawn());
        }
    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(_cloud, transform);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public IEnumerator RandomSpawn()
    {
        while (true)
        {
            var random = Random.Range(_spawnDelay, _secondSpawnDelay);
            Debug.Log(random);
            Instantiate(_cloud, transform);
            yield return new WaitForSeconds(random);
        }
    }
}
