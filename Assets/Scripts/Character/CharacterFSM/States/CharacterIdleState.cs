using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class CharacterIdleState : State<EState, ETransition>
{
    [SerializeField] private CharacterAnimationController _characterAnimationController;
    public override void OnEnterCustomActions()
    {
        _characterAnimationController.PlayAnimation(ECharacterAnimation.Idle);
        base.OnEnterCustomActions();
    }
    protected override EState GetStateID()
    {
        return EState.Idle;
    }

    [PhaseListener(typeof(GamePhase), true)]
    public void SubscribeToPhaseEvent()
    {
        FSM.SetTransition(ETransition.Run);

    }
}