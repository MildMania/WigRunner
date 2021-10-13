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
            { new ST(EState.Idle, ETransition.Walk), EState.Walk },
            
            { new ST(EState.Run, ETransition.Idle), EState.Idle },
            { new ST(EState.Run, ETransition.Walk), EState.Walk },
            { new ST(EState.Run, ETransition.Catwalk), EState.Catwalk },
            { new ST(EState.Run, ETransition.Fall), EState.Fall },
            
            { new ST(EState.Stop, ETransition.Run), EState.Run },
            { new ST(EState.Run, ETransition.Stop), EState.Stop },
            { new ST(EState.Walk, ETransition.Stop), EState.Stop },
            
            { new ST(EState.Walk, ETransition.Idle), EState.Idle },
            { new ST(EState.Walk, ETransition.Run), EState.Run },
            
            { new ST(EState.Catwalk, ETransition.ShowOffFail), EState.ShowOffFail },
            { new ST(EState.Catwalk, ETransition.ShowOffWin), EState.ShowOffWin },
            
            { new ST(EState.ShowOffWin, ETransition.Walk), EState.Walk },
        };
    }
}