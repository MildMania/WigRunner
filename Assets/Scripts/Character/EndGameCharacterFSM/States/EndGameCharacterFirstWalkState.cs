using System;
using UnityEngine;

using DG.Tweening;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterFirstWalkState : State<EState, ETransition>
{
    [SerializeField] private EndGameCharacterAnimationController _animationController;
    [SerializeField] private GameObject _endGameCharacterObject;
    [SerializeField] private Transform _firstWalkTarget;

    public Action OnCompleted;

    protected override EState GetStateID()
    {
        return EState.FirstWalk;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        //_animationController.PlayAnimation(EEndGameCharacterAnimation.Walk);

        //var pos = new Vector3(_firstWalkTarget.position.x, _endGameCharacterObject.transform.position.y, _firstWalkTarget.position.z);

        //_endGameCharacterObject.transform.DOMove(pos, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        //{
        //    _animationController.PlayAnimation(EEndGameCharacterAnimation.Idle);
        //    OnCompleted?.Invoke();
        //});

        OnCompleted?.Invoke();
    }
}