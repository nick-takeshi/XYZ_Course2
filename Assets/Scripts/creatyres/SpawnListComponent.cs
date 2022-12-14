using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnListComponent : MonoBehaviour
{
    [SerializeField] private SpawnData[] spawners;

    public bool InvertScale { get; set; }
    public void Spawn(string id)
    {
        var spawner = spawners.FirstOrDefault(element => element.Id == id);
        spawner?._component.Spawn();

    }

    [Serializable]
    public class SpawnData
    {
        public string Id;
        public SpawnComponent _component;
    }
}
