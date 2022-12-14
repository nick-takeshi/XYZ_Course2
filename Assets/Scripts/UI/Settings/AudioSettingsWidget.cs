using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsWidget : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _value;

    private FloatPersistanceProperty _model;

    public void SetModel(FloatPersistanceProperty model)
    {
        _model = model;
        model.OnChanged += OnValueChanged;
        OnValueChanged(model.Value, model.Value);
    }
    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        _model.Value = value;
    }


    private void OnValueChanged(float newValue, float oldValue)
    {
        var textValue = Mathf.Round(newValue * 100);
        _value.text = textValue.ToString();
        _slider.normalizedValue = newValue;
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        _model.OnChanged -= OnValueChanged;

    }
}
