using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFallState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Fall;
    }
}