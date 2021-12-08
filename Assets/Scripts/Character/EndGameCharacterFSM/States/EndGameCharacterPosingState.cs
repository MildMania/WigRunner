using UnityEngine;

using DG.Tweening;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterPosingState : State<EState, ETransition>
{
    [SerializeField] private GameObject _endGameCharacterObject;

    [SerializeField] private EndGameCharacterAnimationController _animationController;
    [SerializeField] private float _rotateDuration = 2f;

    protected override EState GetStateID()
    {
        return EState.Posing;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _animationController.PlayAnimation(EEndGameCharacterAnimation.Posing);

        _endGameCharacterObject.transform.DORotate(new Vector3(0, 180, 0), _rotateDuration, RotateMode.WorldAxisAdd);

        Character.Instance.CharacterFSM.SetTransition(CharacterFSMController.ETransition.Win);
    }
}