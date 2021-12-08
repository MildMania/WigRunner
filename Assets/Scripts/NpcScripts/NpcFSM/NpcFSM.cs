using System.Collections.Generic;
using UnityEngine;
using EState = NpcFSMController.EState;
using ETransition = NpcFSMController.ETransition;
using ST = StateTransition<NpcFSMController.EState, NpcFSMController.ETransition>;

public class NpcFSM : MMFSM<EState, ETransition>
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
            { new ST(EState.Idle, ETransition.LowJoy), EState.LowJoy },
            { new ST(EState.Idle, ETransition.MiddleJoy), EState.MiddleJoy },
            { new ST(EState.Idle, ETransition.HighJoy), EState.HighJoy }
        };
    }
}