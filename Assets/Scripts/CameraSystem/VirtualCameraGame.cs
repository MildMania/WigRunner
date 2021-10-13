using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class VirtualCameraGame : VirtualCameraBase
{
    [SerializeField] private float _monsterOffsetY = 5;
    [SerializeField] private float _monsterOffsetZ = -6;
    private IEnumerator _offsetRoutine = null;
    private Transform _monsterTransform;
    private static EOwnershipMark winnerMark;

    protected override void ActivateCustomActions()
    {
        Cryopod.OnCryopodDescendStarted += OnCryopodDescendStarted;
        Cryopod.OnCryopodDescendEnded += OnCryopodDescendEnded;
        Chest.Instance.OnChestOpenStarted += OnChestOpenStarted;
        base.ActivateCustomActions();
    }

    protected override void DeactivateCustomActions()
    {
        Cryopod.OnCryopodDescendStarted -= OnCryopodDescendStarted;
        Cryopod.OnCryopodDescendEnded -= OnCryopodDescendEnded;
        base.DeactivateCustomActions();
    }


    private void OnCryopodDescendEnded(EOwnershipMark ownershipMark, Transform monsterTransform)
    {
        if (winnerMark == EOwnershipMark.Player)
        {
            StopCoroutine(_offsetRoutine);
            _monsterTransform = monsterTransform;
            monsterTransform.GetComponentInChildren<MonsterCutsceneController>().OnFightEnded += OnFightEnded;
        }
    }


    private void OnCryopodDescendStarted(EOwnershipMark ownershipMark, Transform monsterTransform)
    {
        winnerMark = ownershipMark;
        if (winnerMark == EOwnershipMark.Player)
        {
            VirtualCamera.LookAt = monsterTransform;
            VirtualCamera.Follow = monsterTransform;
            _offsetRoutine = OffsetRoutine();
            StartCoroutine(_offsetRoutine);
        }
    }

    private void OnFightEnded()
    {
        if (winnerMark == EOwnershipMark.Player)
        {
            _monsterOffsetY += 3;
            _monsterOffsetZ += 1;
            _offsetRoutine = OffsetRoutine();
            StartCoroutine(_offsetRoutine);
        }
    }

    private void OnChestOpenStarted()
    {
        if (winnerMark == EOwnershipMark.Player)
        {
            Chest.Instance.OnChestOpenStarted -= OnChestOpenStarted;
            _monsterTransform.GetComponentInChildren<MonsterCutsceneController>().OnFightEnded -= OnFightEnded;
            StopCoroutine(_offsetRoutine);
        }
    }

    private IEnumerator OffsetRoutine()
    {
        CinemachineTransposer cinemachineTransposer =
            VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        float yStep = 0;
        float zStep = 0;
        float yOffset = cinemachineTransposer.m_FollowOffset.y;
        float zOffset = cinemachineTransposer.m_FollowOffset.z;
        while (true)
        {
            cinemachineTransposer.m_FollowOffset.y = Mathf.Lerp(yOffset, _monsterOffsetY, yStep);
            cinemachineTransposer.m_FollowOffset.z = Mathf.Lerp(zOffset, _monsterOffsetZ, zStep);
            yStep += Time.deltaTime;
            zStep += Time.deltaTime;

            yield return null;
        }
    }
}