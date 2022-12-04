using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : AnimatedWindow
{

    private Action _closeAction;

    public void OnShowSettings()
    {
        var window = Resources.Load<GameObject>("UI/SettingsWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    public void OnStartGame()
    {
        _closeAction = () => { SceneManager.LoadScene(1); };
        Close();
    }

    public void OnLanguage()
    {
        var window = Resources.Load<GameObject>("UI/LocalizationWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    public void OnExit()
    {
        _closeAction = () => 
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

#endif 
        };
        Close();
        }

    public override void OnCloseAnimationCopmplete()
    {
        base.OnCloseAnimationCopmplete();
        _closeAction?.Invoke();


    }


}
