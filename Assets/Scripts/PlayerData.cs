using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    [SerializeField] private InventoryData _inventory;
    public InventoryData Inventory => _inventory;

    public IntProperty Hp = new IntProperty();

    public PerksData Perks = new PerksData();

    public LevelData Levels = new LevelData();

    public SkinData Skins = new SkinData();

    public FloatProperty Fuel = new FloatProperty();

    public PlayerData Clone()
    {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}


