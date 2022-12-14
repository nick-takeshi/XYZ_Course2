using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        var position = _rb.position;
        position.x += _direction * _speed * 0.01f;
        _rb.MovePosition(position);
    }
}
