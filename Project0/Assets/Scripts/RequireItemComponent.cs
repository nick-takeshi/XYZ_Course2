using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RequireItemComponent : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private int _value;
    [SerializeField] private bool _removeAfterUse;

    [SerializeField] private UnityEvent _onSuccess;
    [SerializeField] private UnityEvent _onFail;

    public void Check()
    {
        var session = FindObjectOfType<GameSession>();
        var numItems = session.Data.Inventory.Count(_id);

        if (numItems >= _value)
        {
            if (_removeAfterUse) session.Data.Inventory.Remove(_id, _value);
            _onSuccess?.Invoke();
        }
        else
        {
            _onFail?.Invoke();
        }
    }
}
