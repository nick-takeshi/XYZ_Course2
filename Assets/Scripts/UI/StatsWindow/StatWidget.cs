using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private Text _currentValue;
    [SerializeField] private Text _increaseValue;
    [SerializeField] private ProgressBar _progress;
    [SerializeField] private GameObject _selector;

    private GameSession _session;
    private StatDef _data;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        UpdateView();
    }

    public void SetData(StatDef data, int index)
    {
        _data = data;

        if (_session != null)
        {
            UpdateView();
        }
    }

    public void OnSelect()
    {
        _session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
    }


    private void UpdateView()
    {
        var statsModel = _session.StatsModel;

        _icon.sprite = _data.Icon;
        _name.text = LocalizationManager.I.Localize(_data.Name);
        var currentLevelValue = statsModel.GetValue(_data.Id);
        _currentValue.text = currentLevelValue.ToString(CultureInfo.InvariantCulture);

        var currentLevel = statsModel.GetCurrentLevel(_data.Id);
        var nextLevel = currentLevel + 1;
        var nextLevelValue = statsModel.GetValue(_data.Id, nextLevel);
        var increaseValue = nextLevelValue - currentLevelValue;
        _increaseValue.text = increaseValue.ToString(CultureInfo.InvariantCulture);
        _increaseValue.text = "+" + _increaseValue.text;
        _increaseValue.gameObject.SetActive(increaseValue > 0);

        var maxLevel = DefsFacade.I.Player.GetStat(_data.Id).Levels.Length - 1;
        _progress.SetProgress(currentLevel / (float)maxLevel);

        _selector.SetActive(statsModel.InterfaceSelectedStat.Value == _data.Id);
    }
}
