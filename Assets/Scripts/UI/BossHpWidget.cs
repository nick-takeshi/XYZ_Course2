using System.Collections;
using System;
using UnityEngine;

public class BossHpWidget : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private BossProgressBar _hpBar;
    [SerializeField] private CanvasGroup _canvas;

    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _health.Health;
        _trash.Retain(_health._onChange.Subscribe(OnHpChanged));
        _trash.Retain(_health._onDie.Subscribe(HideUI));
    }

    public void ShowUI()
    {
        this.LerpAnimated(0, 1, 1, SetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        _canvas.alpha = alpha;
    }

    private void HideUI()
    {
        this.LerpAnimated(1, 0, 1, SetAlpha);
    }

    private void OnHpChanged(int hp)
    {
        _hpBar.SetBossProgress(hp / _maxHealth);
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
