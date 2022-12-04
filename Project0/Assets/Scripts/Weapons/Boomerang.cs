using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    [SerializeField] private float lerp;

    private Vector2 fin;
    private int _direction;
    private float _end;

    public void Start()
    {
        _direction = transform.lossyScale.x > 0 ? 1 : -1;
        lerp *= 0.01f;

        if (_direction == 1)
        {
            fin = new Vector2(transform.position.x + 2.2f, transform.position.y);
            _end = transform.position.x + 2;
        }
        else if (_direction == -1)
        {
            fin = new Vector2(transform.position.x - 2.2f, transform.position.y);
            _end = transform.position.x - 2;
        }
    }
    private void Update()
    {
        transform.position = Vector2.Lerp(gameObject.transform.position, fin, lerp);

        if (_direction == 1 & transform.position.x > _end)
        {
            fin.x -= 3f;
        }
        else if(_direction == -1 & transform.position.x < _end)
        { 
            fin.x += 3f;
        }

    }

    

}
