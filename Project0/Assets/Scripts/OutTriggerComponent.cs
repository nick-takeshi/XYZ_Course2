using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OutTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent _action;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {

            _action?.Invoke();

        }
    }
}
