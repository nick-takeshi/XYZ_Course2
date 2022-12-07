using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentPerkWidget : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _cooldownImage;


    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }

    public void Set(PerkDef perk)
    {
        _icon.sprite = perk.Icon;
    }

    private void Update()
    {
        var cooldown = _session.PerksModel.PerkCooldown;
        _cooldownImage.fillAmount = cooldown.RemainingTime / cooldown.Value;
    }
}
