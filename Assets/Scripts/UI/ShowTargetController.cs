using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class ShowTargetController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private static readonly int ShowKey = Animator.StringToHash("ShowTarget");

    public void SetPosition(Vector3 targetPosition)
    {
        targetPosition.z = _camera.transform.position.z;
        _camera.transform.position = targetPosition;
    }

    public void SetState(bool isShown)
    {
        _animator.SetBool(ShowKey, isShown);
    }
}
