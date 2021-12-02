using UnityEngine;

using DG.Tweening;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterSecondWalkState : State<EState, ETransition>
{
    [SerializeField] private EndGameCharacterAnimationController _animationController;
    [SerializeField] private GameObject _endGameCharacterObject;
    [SerializeField] private Transform _secondWalkTarget;
    [SerializeField] private float _walkDuration = 0.5f;



    protected override EState GetStateID()
    {
        return EState.SecondWalk;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _animationController.PlayAnimation(EEndGameCharacterAnimation.Walk);

        var pos = new Vector3(_secondWalkTarget.position.x, _endGameCharacterObject.transform.position.y, _secondWalkTarget.position.z);

        _endGameCharacterObject.transform.DOMove(pos, _walkDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            FSM.SetTransition(ETransition.Posing);
        });
    }
}