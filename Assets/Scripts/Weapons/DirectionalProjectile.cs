using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectile : BaseProjectile
{
    public void Launch(Vector2 direction)
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(direction * _speed, ForceMode2D.Impulse);
        Destroy(gameObject,4);
    }
}
