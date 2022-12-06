using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSecondLevel : MonoBehaviour
{
    [SerializeField] private float _delay;
    public void LoadSecondLvl()
    {
        SceneManager.LoadScene(2);
        var _session = FindObjectOfType<GameSession>();
        _session._removedItems.Clear();
        _session._checkpoints.Clear();
    }

    public void LoadInSomeSec()
    {
        Invoke("LoadSecondLvl", _delay);
    }
}
