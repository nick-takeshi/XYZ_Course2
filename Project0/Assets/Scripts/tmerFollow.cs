using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmerFollow : MonoBehaviour
{
    private Transform _hero;
    void Start()
    {
        _hero = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        transform.position = new Vector3(_hero.position.x, _hero.position.y+0.75f, _hero.position.z);
    }
}
