using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

using DG.Tweening;

public class CharacterEndGameState : State<EState, ETransition>
{
    [SerializeField] private TriggerObjectHitController _finishlineHitController;
    [SerializeField] private GameObject _characterObject;
    

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
        _characterObject.transform.DOMove(EndGameCharacter.Instance.WigAttachPoint.position, 0.3f).SetEase(Ease.Linear).OnComplete(()=> {
            EndGameCharacter.Instance.FSM.SetTransition(EndGameCharacterFSMController.ETransition.ObtainWig);


        });
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

        CameraManager.Instance.OnCameraBlendFinished += OnCameraBlendFinished;
        CameraManager.Instance.ActivateCamera(new CameraActivationArgs(ECameraType.EndGame));

    }
}