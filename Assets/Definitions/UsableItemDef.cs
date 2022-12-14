using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/UsableItemsDef", fileName = "UsableItems")]

public class UsableItemDef : ScriptableObject
{
    [SerializeField] private UsableDef[] _items;

    public UsableDef Get(string id)
    {
        foreach (var itemDef in _items)
        {
            if (itemDef.Id == id) return itemDef;
        }
        return default;
    }

    [Serializable]

    public struct UsableDef
    {
        [SerializeField] private string _id;
        [SerializeField] private int _healValue;

        public string Id => _id;
        public int HealValue => _healValue;
    }
}
