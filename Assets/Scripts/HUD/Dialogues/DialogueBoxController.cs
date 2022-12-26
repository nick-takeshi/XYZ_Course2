using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueBoxController : MonoBehaviour
{
    [SerializeField] protected DialogContent _content;
    [SerializeField] private GameObject _container;
    [SerializeField] private Animator _animator;

    [Space]
    [SerializeField] private float _textSpeed = 0.09f;

    [Header("Sounds")] [SerializeField] private AudioClip _typing;
    [SerializeField] private AudioClip _open;
    [SerializeField] private AudioClip _close;

    private DialogData _data;
    private int _currentSentence;
    private AudioSource _sfxSource;
    private Coroutine _typingRoutine;
    private UnityEvent _onComplete;

    protected Sentence CurrentSentence => _data.Sentences[_currentSentence];


    private void Start()
    {
        _sfxSource = AudioUtils.FindSfxSource();
    }

    protected virtual DialogContent CurrentContent => _content;
    public void ShowDialog(DialogData data, UnityEvent onComplete)
    {
        _onComplete = onComplete;
        _data = data;
        _currentSentence = 0;
        CurrentContent.Text.text = string.Empty;

        _container.SetActive(true);
        _sfxSource.PlayOneShot(_open);
        _animator.SetBool("isOpen", true);   

    }
    protected virtual void OnStartDialogAnimaton()
    {
        _typingRoutine = StartCoroutine(TypeDialogText());
    }
    private IEnumerator TypeDialogText()
    {
        CurrentContent.Text.text = string.Empty;
        var sentence = _data.Sentences[_currentSentence];

        CurrentContent.TrySetIcon(sentence._icon);


        var localizedSentence = LocalizationManager.I.Localize(sentence.Valued);

        foreach (var letter in localizedSentence)
        {
            CurrentContent.Text.text += letter;
            _sfxSource?.PlayOneShot(_typing);
            yield return new WaitForSeconds(_textSpeed);
        }

        _typingRoutine = null;
    }

    public void OnSkip()
    {
        if (_typingRoutine == null) return;

        StopTypeAnimation();
        var localizedSentence = LocalizationManager.I.Localize(_data.Sentences[_currentSentence].Valued);
        CurrentContent.Text.text = localizedSentence;
    }

    public void OnContinue()
    {
        StopTypeAnimation();
        _currentSentence++;

        var isDialogCompleted = _currentSentence >= _data.Sentences.Length;
        if (isDialogCompleted)
        {
            HideDialogBox();
            _onComplete?.Invoke();
        }
        else
        {
            OnStartDialogAnimaton();
        }
    }

    private void HideDialogBox()
    {
        _animator.SetBool("isOpen", false);
        _sfxSource.PlayOneShot(_close);
    }

    private void StopTypeAnimation()
    {
        if (_typingRoutine != null) 
            StopCoroutine(_typingRoutine);
        _typingRoutine = null;

    }
    private void OnCloseAnimationComplete()
    {

    }

}
