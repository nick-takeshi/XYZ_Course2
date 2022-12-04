using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private bool _capitalize;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();

        LocalizationManager.I.OnLocaleChanged += OnLocalChanged;
        Localize();
    }

    private void OnLocalChanged()
    {
        Localize();
    }

    private void Localize()
    {
        var localized = LocalizationManager.I.Localize(_key);
        _text.text = _capitalize ? localized.ToUpper() : localized;
    }

    private void OnDestroy()
    {
        LocalizationManager.I.OnLocaleChanged -= OnLocalChanged;
    }
}
