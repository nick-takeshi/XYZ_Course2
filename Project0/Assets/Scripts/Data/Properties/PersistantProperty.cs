using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class PersistantProperty<TPropertyType>
{
    [SerializeField] protected TPropertyType _value;
    private TPropertyType _storedValue;
    private TPropertyType _defaultValue;

    public delegate void OnPropertyChanged(TPropertyType newValu, TPropertyType oldValue);
    public event OnPropertyChanged OnChanged;

    public PersistantProperty(TPropertyType defaultValue)
    {
        _defaultValue = defaultValue;
    }
    public TPropertyType Value
    {
        get => _storedValue;
        set
        {
           var isEquals = _storedValue.Equals(value);
            if (isEquals) return;

            var oldValue = _storedValue;
            Write(value);
            _storedValue = _value = value;

            OnChanged?.Invoke(value, oldValue);
        }
    }

    protected void Init()
    {
        _storedValue = _value = Read(_defaultValue);
    }
    protected abstract void Write(TPropertyType value);
    protected abstract TPropertyType Read(TPropertyType defaultValue);

    public void Validate()
    {
        if (!_storedValue.Equals(_value))
        {
            Value = _value;
        }
    }
}
