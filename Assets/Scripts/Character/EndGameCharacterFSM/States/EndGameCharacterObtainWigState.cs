using UnityEngine;

using MMFramework.Utilities;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterObtainWigState : State<EState, ETransition>
{
    [SerializeField] private EndGameCharacterAnimationController _animationController;

    [SerializeField] private float _stateDuration = 2f;

    protected override EState GetStateID()
    {
        return EState.ObtainWig;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();
        var visualController = Character.Instance.CharacterVisualController;

        var hair = visualController.GetHairWithHairType(visualController.CurrentHairType);

        var pivot = EndGameCharacter.Instance.VisualController.GetHairPivotWithHairType(visualController.CurrentHairType).Pivot;

        hair.HairObject.transform.parent = pivot;

        _animationController.PlayAnimation(EEndGameCharacterAnimation.Excited);

        CoroutineRunner.Instance.WaitForSeconds(_stateDuration, () =>
        {
            FSM.SetTransition(ETransition.SecondWalk);
        });

    }
}