using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUtils : MonoBehaviour
{
    public static void CreateWindow(string resourcePath)
    {
        var window = Resources.Load<GameObject>(resourcePath);
        var canvas = Object.FindObjectOfType<Canvas>();
        Object.Instantiate(window, canvas.transform);
    }
}
