using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime;

    private static readonly int EnabledKey = Animator.StringToHash("Enabled");

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoaded()
    {
        InitLoader();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private static void InitLoader()
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(StartAnimation(sceneName));
    }

    private IEnumerator StartAnimation(string sceneName)
    {
        _animator.SetBool(EnabledKey, true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(sceneName);
        _animator.SetBool(EnabledKey, false);
    }
}
