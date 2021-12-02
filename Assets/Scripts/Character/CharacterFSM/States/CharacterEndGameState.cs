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

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _characterObject.transform.DOMove(EndGameCharacter.Instance.AttachPoint.position, 0.3f);
    }
}