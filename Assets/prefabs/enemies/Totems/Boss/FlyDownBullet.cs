using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDownBullet : MonoBehaviour
{
    [SerializeField] protected float _speed = 1f;

    protected Rigidbody2D _rb;
    protected int _direction;

    protected virtual void Start()
    {
        _direction = transform.lossyScale.y > 0 ? 1 : -1;
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        var position = _rb.position;
        position.y -= _direction * _speed * 0.01f;
        _rb.MovePosition(position);
    }
}
