using UnityEngine;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterPosingState : State<EState, ETransition>
{
    [SerializeField] private EndGameCharacterAnimationController _animationController;

    protected override EState GetStateID()
    {
        return EState.Posing;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _animationController.PlayAnimation(EEndGameCharacterAnimation.Posing);

        Character.Instance.CharacterFSM.SetTransition(CharacterFSMController.ETransition.Win);
    }
}