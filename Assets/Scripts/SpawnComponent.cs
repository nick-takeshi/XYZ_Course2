using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _invertXScale;

    [ContextMenu("Spawn")]
    public void Spawn()
    {

        SpawnInstance();
    }
    public GameObject SpawnInstance()
    {
        var instance = SpawnUtils.Spawn(_prefab, _target.position);

        var scale = _target.lossyScale;
        scale.x *= _invertXScale ? -1 : 1;
        instance.transform.localScale = scale;
        instance.SetActive(true);

        return instance;
    }

    public void SpawnTimer()
    {
        var timer = Instantiate(_prefab, transform);
        timer.transform.position = _target.transform.position;
    }

    public void SpawnLocalScale()
    {
        var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
        instantiate.transform.localScale = _target.lossyScale;
        instantiate.transform.position = _target.transform.position;
    }

    public void SetPrefab(GameObject prefab)
    {
        _prefab = prefab;
    }

    
}
