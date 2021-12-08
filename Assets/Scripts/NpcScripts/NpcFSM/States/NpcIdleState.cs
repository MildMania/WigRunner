using UnityEngine;

using EState = NpcFSMController.EState;
using ETransition = NpcFSMController.ETransition;

public class NpcIdleState : State<EState, ETransition>
{
    [SerializeField] private NpcAnimationController _animationController;

    protected override EState GetStateID()
    {
        return EState.Idle;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        var time = Random.Range(0, 5f);

        _animationController.PlayAnimation(NpcAnimationController.EAnimation.Idle, fixedTimeOffset: time);
    }
}