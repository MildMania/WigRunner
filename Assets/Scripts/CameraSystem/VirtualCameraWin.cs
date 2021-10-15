using UnityEngine;
using DG.Tweening;

public class VirtualCameraWin : VirtualCameraBase
{
    [SerializeField] private float _winCameraTravelDuration = 1f;

    protected override void ActivateCustomActions()
    {
        var dollycart = transform.GetComponentInChildren<Cinemachine.CinemachineDollyCart>();

        DOTween.To(() => dollycart.m_Position,
                   p => dollycart.m_Position = p,
                   1,
                   _winCameraTravelDuration);

        base.ActivateCustomActions();
    }
}