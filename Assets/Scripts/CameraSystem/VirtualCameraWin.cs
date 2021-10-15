using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class VirtualCameraWin : VirtualCameraBase
{
    [SerializeField] private float _winCameraTravelDuration = 1f;
    [SerializeField] private CinemachineDollyCart _dollyCart;

    protected override void ActivateCustomActions()
    {
        DOTween.To(() => _dollyCart.m_Position,
                   p => _dollyCart.m_Position = p,
                   1,
                   _winCameraTravelDuration);

        base.ActivateCustomActions();
    }
}