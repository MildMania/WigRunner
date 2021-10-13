using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFSM_FallState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Fall;
    }
}