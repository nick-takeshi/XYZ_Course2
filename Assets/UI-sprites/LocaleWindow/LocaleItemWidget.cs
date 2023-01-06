using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _selector;

    [SerializeField] private SelectedLocale _onSelected;

    private LocaleInfo _data;

    private void Start()
    {
        LocalizationManager.I.OnLocaleChanged += UpdateSelection;
    }

    public void SetData(LocaleInfo localeInfo, int index)
    {
        _data = localeInfo;
        UpdateSelection();
        _text.text = localeInfo.LocaleId.ToUpper();
    }

    private void UpdateSelection()
    {
        var isSelected = LocalizationManager.I.LocaleKey == _data.LocaleId;
        _selector.SetActive(isSelected);
    }

    public void OnSelected()
    {
        _onSelected?.Invoke(_data.LocaleId);
    }

    private void OnDestroy()
    {
        LocalizationManager.I.OnLocaleChanged -= UpdateSelection;
    }
}

public class LocaleInfo
{
    public string LocaleId;
}

[Serializable]
public class SelectedLocale : UnityEvent<string>
{

}

