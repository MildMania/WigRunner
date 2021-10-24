using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterWinState : State<EState, ETransition>
{
    [SerializeField] private TriggerObjectHitController _finishlineHitController;

    [SerializeField] private CharacterAnimationController _characterAnimationController = null;

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
        FSM.SetTransition(ETransition.Win);
        _characterAnimationController.PlayAnimation(ECharacterAnimation.Idle);
    }

    protected override EState GetStateID()
    {
        return EState.Win;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();
    }
}