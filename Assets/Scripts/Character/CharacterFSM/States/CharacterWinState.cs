using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterWinState : State<EState, ETransition>
{
    [SerializeField] private iOSHapticFeedback.iOSFeedbackType _successHapticFeedback = iOSHapticFeedback.iOSFeedbackType.Success;
    private OnHapticRequestedEventRaiser _onHapticRequestedEventRaiser = new OnHapticRequestedEventRaiser();

    protected override EState GetStateID()
    {
        return EState.Win;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _onHapticRequestedEventRaiser.Raise(new OnHapticRequestedEventArgs(_successHapticFeedback));
    }
}