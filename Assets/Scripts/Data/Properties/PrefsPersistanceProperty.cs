using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrefsPersistanceProperty<TPropertyType> : PersistantProperty<TPropertyType>
{
    protected string Key;
    protected PrefsPersistanceProperty(TPropertyType defaultValue, string key) : base(defaultValue)
    {
        Key = key;
    }
}
