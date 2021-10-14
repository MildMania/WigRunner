using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterWinState : State<EState, ETransition>
{
    [SerializeField] private TriggerObjectHitController _finishlineHitController;


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
    }

    protected override EState GetStateID()
    {
        return EState.Win;
    }
}