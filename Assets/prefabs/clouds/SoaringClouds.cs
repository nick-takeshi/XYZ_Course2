using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringClouds : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToDestroy;
    void Start()
    {
        _speed = _speed / 40;
        Destroy(gameObject, _timeToDestroy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.left * _speed;
    }
}
