using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Throwable", fileName = "Throwable")]

public class ThrowableRepository : DefRepository<ThrowableDef>
{
   
}
[Serializable]

public struct ThrowableDef : IHaveId
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _projectile;

    public string Id => _id;
    public GameObject Projectile => _projectile;
}
