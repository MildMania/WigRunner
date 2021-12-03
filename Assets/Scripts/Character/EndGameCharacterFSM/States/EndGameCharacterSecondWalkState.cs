using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterSecondWalkState : State<EState, ETransition>
{
    [SerializeField] private EndGameCharacterAnimationController _animationController;
    [SerializeField] private GameObject _endGameCharacterObject;
    [SerializeField] private List<Transform> _secondWalkTargets;
    [SerializeField] private float _walkSpeed = 10f;


    private void Walk(int i)
    {


        if (i < _secondWalkTargets.Count)
        {

            //var pos = new Vector3(_secondWalkTargets[i].position.x, _endGameCharacterObject.transform.position.y, _secondWalkTargets[i].position.z);
            var pos = _secondWalkTargets[i].position;

            var dist = (pos - _endGameCharacterObject.transform.position).magnitude;

            var duration = (dist / _walkSpeed);

            _endGameCharacterObject.transform.DOMove(pos, duration).SetEase(Ease.Linear).SetEase(Ease.Linear).OnComplete(() =>
            {
                Walk(i + 1);
            });

        }
        else
        {
            FSM.SetTransition(ETransition.Posing);
        }


    }


    protected override EState GetStateID()
    {
        return EState.SecondWalk;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _animationController.PlayAnimation(EEndGameCharacterAnimation.Walk);

        Walk(0);

    }
}