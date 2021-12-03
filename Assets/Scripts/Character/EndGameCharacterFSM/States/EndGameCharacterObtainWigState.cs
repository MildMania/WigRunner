using UnityEngine;

using MMFramework.Utilities;
using MMFramework.TasksV2;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterObtainWigState : State<EState, ETransition>
{
    [SerializeField] private EndGameCharacterAnimationController _animationController;

    [SerializeField] private float _stateDuration = 2f;

    [SerializeField] private MMTaskExecutor _onObtainedTasks;

    protected override EState GetStateID()
    {
        return EState.ObtainWig;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _onObtainedTasks.Execute(this);

        CameraManager.Instance.ActivateCamera(new CameraActivationArgs(ECameraType.WigObtain));

        var visualController = Character.Instance.CharacterVisualController;

        var hair = visualController.GetHairWithHairType(visualController.CurrentHairType);

        var pivot = EndGameCharacter.Instance.VisualController.GetHairPivotWithHairType(visualController.CurrentHairType).Pivot;

        hair.HairObject.transform.parent = pivot;

        var dynamicBones = hair.HairObject.GetComponentsInChildren<DynamicBone>();
        foreach (var dynamicBone in dynamicBones)
        {
            dynamicBone.SetWeight(0);
        }

        _animationController.PlayAnimation(EEndGameCharacterAnimation.Excited);

        CoroutineRunner.Instance.WaitForSeconds(_stateDuration, () =>
        {
            FSM.SetTransition(ETransition.SecondWalk);
        });

    }
}