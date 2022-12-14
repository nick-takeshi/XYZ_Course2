using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreStateComponent : MonoBehaviour
{
    [SerializeField] private string _id;

    private GameSession _session;

    public string Id => _id;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        var isDestroyed = _session.RestoreState(Id);

        if (isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    //private void OnDestroy()
    //{
    //    _session.StoreState(Id);
    //}
}
