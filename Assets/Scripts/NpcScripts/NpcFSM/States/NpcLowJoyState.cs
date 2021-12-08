using UnityEngine;

using MMFramework.TasksV2;

using EState = NpcFSMController.EState;
using ETransition = NpcFSMController.ETransition;

public class NpcLowJoyState : State<EState, ETransition>
{
    [SerializeField] private NpcAnimationController _animationController;
    [SerializeField] private MMTaskExecutor _onLowJoy;

    protected override EState GetStateID()
    {
        return EState.LowJoy;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        if (_onLowJoy != null)
            _onLowJoy.Execute(this);

        _animationController.PlayAnimation(NpcAnimationController.EAnimation.LowJoy);
    }
}