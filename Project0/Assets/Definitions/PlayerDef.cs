using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/PlayerDef", fileName ="PlayerDef")]
public class PlayerDef : ScriptableObject
{
    [SerializeField] private int _inventorySize;
    [SerializeField] private int _maxHealth = 10;

    public int InventorySize => _inventorySize;
    public int MaxHealth => _maxHealth;
}
