using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossProgressBar : MonoBehaviour
{
    [SerializeField] private Image _bar;

    public void SetBossProgress(float progress)
    {
        _bar.fillAmount = progress;
    }
}
