using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectroyObject : MonoBehaviour
{
    [SerializeField] private GameObject _objToDestroy;
    [SerializeField] private RestoreStateComponent _storeState;
    [SerializeField] private float _timeToDestroy;
    public void DestroyObject()
    {
        Destroy(_objToDestroy, _timeToDestroy);
        if (_storeState != null)
            FindObjectOfType<GameSession>().StoreState(_storeState.Id);
    }

}
