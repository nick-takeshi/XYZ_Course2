using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinWidget : MonoBehaviour, IItemRenderer<SkinDef>
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _isLocked;
    [SerializeField] private GameObject _isUsed;
    [SerializeField] private GameObject _isSelected;

    private GameSession _session;
    private SkinDef _data;
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        UpdateView();
    }
    public void OnSelect()
    {
        _session.SkinModel.InterfaceSelection.Value = _data.Id;
    }

    public void SetData(SkinDef data, int index)
    {
        _data = data;

        if (_session != null)
        {
            UpdateView();
        }
    }

    private void UpdateView()
    {
        _icon.sprite = _data.Icon;
        _isUsed.SetActive(_session.SkinModel.IsUsed(_data.Id));
        _isSelected.SetActive(_session.SkinModel.InterfaceSelection.Value == _data.Id);
        _isLocked.SetActive(!_session.SkinModel.IsUnlocked(_data.Id));
    }

}
