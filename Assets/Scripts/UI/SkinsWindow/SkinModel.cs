using System.Collections;
using System;
using UnityEngine;
using UnityEditor.Animations;


public class SkinModel : IDisposable
{
    private readonly PlayerData _data;

    public readonly StringProperty InterfaceSelection = new StringProperty();
    public readonly AnimatorProperty InterfaceSelectionAnim = new AnimatorProperty();

    private readonly CompositeDisposable _trash = new CompositeDisposable();
    private SkinDef _skin;

    public string Used => _data.Skins.Used.Value;

    public AnimatorController UsedAnim => _data.Skins.Anim.Value;

    public event Action OnChanged;



    public IDisposable Subscribe(Action call)
    {
        OnChanged += call;
        return new ActionDisposable(() => OnChanged -= call);
    }
    public SkinModel(PlayerData data)
    {
        
        _data = data;
        InterfaceSelection.Value = DefsFacade.I.Skins.All[0].Id;
        InterfaceSelectionAnim.Value = DefsFacade.I.Skins.All[0].Animator;

        _trash.Retain(_data.Skins.Used.Subscribe((x, y) => OnChanged?.Invoke()));
        _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));

        _trash.Retain(_data.Skins.Anim.Subscribe((x, y) => OnChanged?.Invoke()));
        _trash.Retain(InterfaceSelectionAnim.Subscribe((x, y) => OnChanged?.Invoke()));

    }
    public void Unlock(string id)
    {
        var def = DefsFacade.I.Skins.Get(id);
        var isEnoughResources = _data.Inventory.IsEnough(def.Price);

        if (isEnoughResources)
        {
            _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
            _data.Skins.AddSkin(id);
            OnChanged?.Invoke();
        }
    }
    public void SelectSkin(string selected)
    {
        _data.Skins.Used.Value = selected;
    }

    public void SelectSkinAnim(AnimatorController selected)
    {
        _data.Skins.Anim.Value = selected;
    }
    public bool IsUsed(string skinId)
    {
        return _data.Skins.Used.Value == skinId;
    }

    public bool IsUnlocked(string skinId)
    {
        return _data.Skins.IsUnlocked(skinId);
    }

    public bool CanBuy(string skinId)
    {
        var def = DefsFacade.I.Skins.Get(skinId);
        return _data.Inventory.IsEnough(def.Price);
    }

    public void Dispose()
    {
        _trash.Dispose();
    }
}
