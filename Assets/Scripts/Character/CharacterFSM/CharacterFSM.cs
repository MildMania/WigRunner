using System.Collections.Generic;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;
using ST = StateTransition<CharacterFSMController.EState, CharacterFSMController.ETransition>;

public class CharacterFSM : MMFSM<EState, ETransition>
{
    [SerializeField] private EState _initialState = EState.Idle;

    protected override EState GetEnteranceStateID()
    {
        return _initialState;
    }

    protected override Dictionary<ST, EState> GetTransitionDict()
    {
        return new Dictionary<ST, EState>
        {
            { new ST(EState.Idle, ETransition.Run), EState.Run },
            { new ST(EState.Run, ETransition.Idle), EState.Idle },
            { new ST(EState.Run, ETransition.Fall), EState.Fall },
            { new ST(EState.Fall, ETransition.Fail), EState.Fail },
            { new ST(EState.Run, ETransition.Fail), EState.Fail },
            { new ST(EState.Run, ETransition.Win), EState.Win },
        };
    }
}