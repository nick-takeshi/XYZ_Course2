using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSkinsWindow : AnimatedWindow
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private ItemWidget _price;
    [SerializeField] private Text _info;
    [SerializeField] private Transform _skinsContainer;

    private PredefinedDataGroup<SkinDef, SkinWidget> _dataGroup;
    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private GameSession _session;
    private Hero _hero;
    protected override void Start()
    {
        base.Start();

        _dataGroup = new PredefinedDataGroup<SkinDef, SkinWidget>(_skinsContainer);
        _session = FindObjectOfType<GameSession>();
        _hero = FindObjectOfType<Hero>();

        _trash.Retain(_session.SkinModel.Subscribe(OnSkinsChanged));

        //_trash.Retain(_buyButton.onClick.Subscribe(OnBuy));
        //_trash.Retain(_useButton.onClick.Subscribe(OnUse));

        OnSkinsChanged();
    }
    private void OnSkinsChanged()
    {
        _dataGroup.SetData(DefsFacade.I.Skins.All);

        var selected = _session.SkinModel.InterfaceSelection.Value;

        _useButton.gameObject.SetActive(_session.SkinModel.IsUnlocked(selected));
        _useButton.interactable = _session.SkinModel.Used != selected;

        _buyButton.gameObject.SetActive(!_session.SkinModel.IsUnlocked(selected));
        _buyButton.interactable = _session.SkinModel.CanBuy(selected);

        var def = DefsFacade.I.Skins.Get(selected);
        _price.SetData(def.Price);

        _info.text = LocalizationManager.I.Localize(def.Info);
    }
    public void OnUse()
    {
        var selected = _session.SkinModel.InterfaceSelection.Value;
        _session.SkinModel.SelectSkin(selected);


        //var selected1 = _session.SkinModel.InterfaceSelectionAnim.Value;
        //_session.SkinModel.SelectSkinAnim(selected1);
        //Debug.Log(selected1);

        //ChangeSkin();
    }

    public void OnBuy()
    {
        var selected = _session.SkinModel.InterfaceSelection.Value;
        _session.SkinModel.Unlock(selected);

    }

    //public void ChangeSkin()
    //{
    //    //Debug.Log(_hero._armed);
    //    //Debug.Log(_session.SkinModel.UsedAnim);

    //    _hero._armed = _session.SkinModel.UsedAnim;

    //}

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
