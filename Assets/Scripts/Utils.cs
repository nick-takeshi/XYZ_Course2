using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool IsInLayer(this GameObject obj, LayerMask layer)
    {
        return layer == (layer | 1 << obj.layer);
    }
}
