using System.Collections;
using System;
using UnityEngine;

public class PerksModel : IDisposable
{
    private readonly PlayerData _data;

    public readonly StringProperty InterfaceSelection = new StringProperty();

    public readonly Cooldown PerkCooldown = new Cooldown();

    private readonly CompositeDisposable _trash = new CompositeDisposable();

    public string Used => _data.Perks.Used.Value;
    public bool IsShieldSupported => _data.Perks.Used.Value == "shield" && PerkCooldown.IsReady;
    public bool IsDoubleJumpSupported => _data.Perks.Used.Value == "double-jump" && PerkCooldown.IsReady;
    public bool IsDodgeSupported => _data.Perks.Used.Value == "dodge" && PerkCooldown.IsReady;



    public event Action OnChanged;

    public IDisposable Subscribe(Action call)
    {
        OnChanged += call;
        return new ActionDisposable(() => OnChanged -= call);
    }

    public PerksModel(PlayerData data)
    {
        _data = data;
        InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;

        _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
        _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
    }

    public void Unlock(string id)
    {
        var def = DefsFacade.I.Perks.Get(id);
        var isEnoughResources = _data.Inventory.IsEnough(def.Price);

        if (isEnoughResources)
        {
            _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
            _data.Perks.AddPerk(id);
            OnChanged?.Invoke();
        }
    }

    public void SelectPerk(string selected)
    {
        var perkDef = DefsFacade.I.Perks.Get(selected);
        PerkCooldown.Value = perkDef.Cooldown;
        _data.Perks.Used.Value = selected;
    }

    public bool IsUsed(string perkId)
    {
        return _data.Perks.Used.Value == perkId;
    }

    public bool IsUnlocked(string perkId)
    {
        return _data.Perks.IsUnlocked(perkId);
    }

    public bool CanBuy(string perkId)
    {
        var def = DefsFacade.I.Perks.Get(perkId);
        return _data.Inventory.IsEnough(def.Price);
    }

    public void Dispose()
    {
        _trash.Dispose();
    }
}
