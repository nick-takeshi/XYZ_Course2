using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeDialog : MonoBehaviour
{
    [SerializeField] private DialogDef _translate;

    private void OnLocalChanged()
    {
        Localize();
    }

    private void Localize()
    {

    }
}
