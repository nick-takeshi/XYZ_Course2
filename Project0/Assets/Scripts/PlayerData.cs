using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    [SerializeField] private InventoryData _inventory;
    public InventoryData Inventory => _inventory;

    public IntProperty Hp = new IntProperty();

    public PlayerData Clone()
    {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}


