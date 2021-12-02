using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterWinState : State<EState, ETransition>
{

    protected override EState GetStateID()
    {
        return EState.Win;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();



    }
}