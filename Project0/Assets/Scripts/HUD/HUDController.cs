using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private CurrentPerkWidget _currentPerk;


    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));

        _trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));

        OnPerkChanged();
    }

    private void OnPerkChanged()
    {
        var usedPerkId = _session.PerksModel.Used;
        var hasPerk = !string.IsNullOrEmpty(usedPerkId);
        if (hasPerk)
        {
            var perkDef = DefsFacade.I.Perks.Get(usedPerkId);
            _currentPerk.Set(perkDef);
        }

        _currentPerk.gameObject.SetActive(hasPerk);
    }

    private void OnHealthChanged(int newValue, int oldValue)
    {
        //var maxHealth = DefsFacade.I.Player.MaxHealth;
        var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
        var value = (float)newValue / maxHealth;
        _healthBar.SetProgress(value);
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }

    public void Debug()
    {
        WindowUtils.CreateWindow("UI/PlayerStatsWindow");
    }
}
