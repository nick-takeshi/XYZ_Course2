using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefabSilver;
    [SerializeField] private GameObject _prefabGolden;
    [SerializeField] private int _powerOfJump;
    [SerializeField] private int _numberOfSilverCoins;
    [SerializeField] private int _numberOfGoldenCoins;
    [SerializeField] private float _silverProbabilityPercentage;
    [SerializeField] private float _goldProbabilityPercentage;
    public void SpawnCoin()
    {
        int _random1 = Random.Range(1, 100);
        if (_silverProbabilityPercentage >= _random1)
        {
            for (int i = 0; i < _numberOfSilverCoins; i++)
            {
                var instantiate = Instantiate(_prefabSilver, _target.position, Quaternion.identity);
                instantiate.transform.localScale = _target.lossyScale;

                Rigidbody2D _rigidbody;
                _rigidbody = instantiate.GetComponent<Rigidbody2D>();
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.y, _powerOfJump);
            }
        }

        int _random2 = Random.Range(1, 100);
        if (_goldProbabilityPercentage >= _random2)
        {
            for (int i = 0; i < _numberOfGoldenCoins; i++)
            {
                var instantiate = Instantiate(_prefabGolden, _target.position, Quaternion.identity);
                instantiate.transform.localScale = _target.lossyScale;

                Rigidbody2D _rigidbody;
                _rigidbody = instantiate.GetComponent<Rigidbody2D>();
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.y, _powerOfJump);
            }
        }

        
    }
}
