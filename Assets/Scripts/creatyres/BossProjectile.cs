using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] protected float _speed = 1f;

    protected Rigidbody2D _rb;
    protected Vector2 _position;
    protected int _direction;


    protected virtual void Start()
    {
        var hero = GameObject.FindGameObjectWithTag("Player");
        _position = hero.transform.position;
        _direction = transform.lossyScale.x > 0 ? 1 : -1;
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_position * _speed);
    }







    //[SerializeField] private float _step;
    //[SerializeField] private float _progress;
    //private Vector2 _startPos;
    //private Vector2 _endPos;

    //protected virtual void Start()
    //{
    //    _startPos = transform.position;
    //    var hero = GameObject.FindGameObjectWithTag("Player");
    //    _endPos = hero.transform.position;
    //    Destroy(gameObject, 5);
    //}

    //private void FixedUpdate()
    //{
    //    transform.position = Vector2.Lerp(_startPos, _endPos, _progress);
    //    _progress += _step;
    //}
}
