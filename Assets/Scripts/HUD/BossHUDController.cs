using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHUDController : MonoBehaviour
{
    [SerializeField] private BossProgressBar _healthBar;


    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));


    }

    private void OnHealthChanged(int newValue, int oldValue)
    {
        //var maxHealth = DefsFacade.I.Player.MaxHealth;
        var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
        var value = (float)newValue / maxHealth;
       // _healthBar.SetBossProgress(value);
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }

}
