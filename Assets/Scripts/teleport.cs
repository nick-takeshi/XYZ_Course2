using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] private Transform _destPosition;

    public void Teleport(GameObject target)
    {
        target.transform.position = _destPosition.position;
    }

}
