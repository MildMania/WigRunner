using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

using DG.Tweening;

public class CharacterEndGameState : State<EState, ETransition>
{
    [SerializeField] private TriggerObjectHitController _finishlineHitController;
    [SerializeField] private CharacterVisualController _visualController;

    private void Awake()
    {
        _finishlineHitController.OnHitTriggerObject += OnHitTriggerObject;
        EndGameCharacter.Instance.FirstWalkState.OnCompleted += OnFirstWalkCompleted;
    }

    private void OnDestroy()
    {
        _finishlineHitController.OnHitTriggerObject -= OnHitTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject obj)
    {
        FSM.SetTransition(ETransition.EndGame);
    }


    protected override EState GetStateID()
    {
        return EState.EndGame;
    }

    private void OnFirstWalkCompleted()
    {
        var curHairType = Character.Instance.CharacterVisualController.CurrentHairType;
        var hair = Character.Instance.CharacterVisualController.GetHairWithHairType(curHairType);

        var pivot = EndGameCharacter.Instance.VisualController.GetHairPivotWithHairType(curHairType).Pivot;

        Vector3 attachPos = pivot.position;

        _visualController.AddedParticlesParent.transform.parent = hair.HairObject.transform;

        var seq = DOTween.Sequence();

        seq.Append(hair.HairObject.transform.DOMove(attachPos, 0.8f).SetEase(Ease.InCirc).OnComplete(() => {

            var vcam = CameraManager.Instance.GetCamera(ECameraType.EndGame);

            vcam.VirtualCamera.m_Follow = null;
            vcam.VirtualCamera.m_LookAt = null;

            //hair.HairObject.transform.DORotateQuaternion(Quaternion.LookRotation(pivot.forward), 0.1f);
            hair.HairModel.transform.rotation = Quaternion.LookRotation(pivot.forward);

            EndGameCharacter.Instance.FSM.SetTransition(EndGameCharacterFSMController.ETransition.ObtainWig);

        }));

        seq.Join((hair.HairModel.transform.DOLocalMoveY(0.2f, 0.4f)).SetEase(Ease.OutCirc).OnComplete(()=> {
            hair.HairModel.transform.DOLocalMoveY(0, 0.4f).SetEase(Ease.InCirc);
        }));
        EndGameCharacter.Instance.FirstWalkState.OnCompleted -= OnFirstWalkCompleted;
    }

    private void OnCameraBlendFinished(ECameraType camType)
    {
        if(camType == ECameraType.EndGame)
        {

            EndGameCharacter.Instance.FSM.SetTransition(EndGameCharacterFSMController.ETransition.FirstWalk);
            CameraManager.Instance.OnCameraBlendFinished -= OnCameraBlendFinished;
        }
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        var vcam = CameraManager.Instance.GetCamera(ECameraType.EndGame);

        var curHairType = Character.Instance.CharacterVisualController.CurrentHairType;
        var hair = Character.Instance.CharacterVisualController.GetHairWithHairType(curHairType);

        vcam.VirtualCamera.m_Follow = hair.HairObject.transform;
        vcam.VirtualCamera.m_LookAt = hair.HairObject.transform;

        CameraManager.Instance.OnCameraBlendFinished += OnCameraBlendFinished;
        CameraManager.Instance.ActivateCamera(new CameraActivationArgs(ECameraType.EndGame));

    }
}