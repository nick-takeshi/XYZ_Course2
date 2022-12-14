using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private float _fanPower;

    private void Awake()
    {
    }

    public void FlyAway()
    {
        //_rigid.velocity = new Vector2(_rigid.velocity.x, _fanPower);
        _rigid.gravityScale = _fanPower*-1;
    }

    public void FlyBack()
    {
        _rigid.gravityScale = 2.3f;
    }
}
