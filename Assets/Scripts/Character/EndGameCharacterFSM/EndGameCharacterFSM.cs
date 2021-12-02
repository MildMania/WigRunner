using System.Collections.Generic;
using UnityEngine;
using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;
using ST = StateTransition<EndGameCharacterFSMController.EState, EndGameCharacterFSMController.ETransition>;

public class EndGameCharacterFSM : MMFSM<EState, ETransition>
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
            { new ST(EState.Idle, ETransition.FirstWalk), EState.FirstWalk},
            { new ST(EState.FirstWalk, ETransition.ObtainWig), EState.ObtainWig },
            { new ST(EState.ObtainWig, ETransition.SecondWalk), EState.SecondWalk },
            { new ST(EState.SecondWalk, ETransition.Posing), EState.Posing },

        };
    }
}