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

    public StatDef GetStat(StatId id)
    {
        foreach (var statDef in _stats)
        {
            if (statDef.Id == id)
            {
                return statDef;
            }
        }
        return default;

    }
    //=> _stats.FirstOrDefault(x => x.Id == id);
}
