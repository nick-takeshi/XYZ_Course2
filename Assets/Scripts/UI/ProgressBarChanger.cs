using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarChanger : MonoBehaviour
{
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private HealthComponent _hp;

    private int _maxHp;
    private readonly CompositeDisposable _trash = new CompositeDisposable();

    void Start()
    {
        if (_hp == null)
        {
            _hp = GetComponentInParent<HealthComponent>();
        }
        _maxHp = _hp.Health;


        _trash.Retain(_hp._onDie.Subscribe(OnDie));
        _trash.Retain(_hp._onChange.Subscribe(OnHpChanged));
        
    }

    public void OnHpChanged(int hp)
    {
        var progress = (float) hp / _maxHp;
        _lifeBar.SetProgress(progress);
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
