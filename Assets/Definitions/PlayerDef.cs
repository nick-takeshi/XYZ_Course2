using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/PlayerDef", fileName ="PlayerDef")]
public class PlayerDef : ScriptableObject
{
    [SerializeField] private int _inventorySize;
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private StatDef[] _stats;

    public int InventorySize => _inventorySize;
    public int MaxHealth => _maxHealth;
    public StatDef[] Stats => _stats;

    public StatDef GetStat(StatId id) => _stats.FirstOrDefault(x => x.Id == id);
}
