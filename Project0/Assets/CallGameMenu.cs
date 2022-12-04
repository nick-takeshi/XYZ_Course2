using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGameMenu : MonoBehaviour
{
    public void OnShowGameMenu()
    {
        var window = Resources.Load<GameObject>("UI/GameMenu");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
        Time.timeScale = 0;
    }

}
