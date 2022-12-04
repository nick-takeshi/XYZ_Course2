using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    public void Reload()
    {
        var _session = FindObjectOfType<GameSession>();
        Destroy(_session);
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        
    }
}
