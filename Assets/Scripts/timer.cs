using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField] private GameObject _point;

    public void Start()
    {
        
    }
    void Update()
    {
        
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
        
    }
}
