using UnityEngine;

using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFailState : State<EState, ETransition>
{
    [SerializeField] private iOSHapticFeedback.iOSFeedbackType _failHapticFeedback = iOSHapticFeedback.iOSFeedbackType.Failure;
    private OnHapticRequestedEventRaiser _onHapticRequestedEventRaiser = new OnHapticRequestedEventRaiser();

    protected override EState GetStateID()
    {
        _onHapticRequestedEventRaiser.Raise(new OnHapticRequestedEventArgs(_failHapticFeedback));

        return EState.Fail;
    }
}