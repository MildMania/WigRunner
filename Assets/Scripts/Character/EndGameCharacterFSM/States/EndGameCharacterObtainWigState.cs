using System;

using UnityEngine;

using MMFramework.Utilities;
using MMFramework.TasksV2;

using DG.Tweening;

using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterObtainWigState : State<EState, ETransition>
{
    [SerializeField] private GameObject _endGameCharacterModel;
    [SerializeField] private EndGameCharacterAnimationController _animationController;

    [SerializeField] private float _stateDuration = 2f;

    [SerializeField] private MMTaskExecutor _onObtainedTasks;

    public Action<float> OnWigObtained;

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
        hair.HairObject.transform.localPosition = Vector3.zero;

        var dynamicBones = hair.HairObject.GetComponentsInChildren<DynamicBone>();
        foreach (var dynamicBone in dynamicBones)
        {
            dynamicBone.SetWeight(0);
        }


        CoroutineRunner.Instance.WaitForSeconds(0.1f, () =>
        {
            OnWigObtained?.Invoke(1f);
            _endGameCharacterModel.transform.DORotateQuaternion(Quaternion.LookRotation(Vector3.forward), 1f).OnComplete(()=> {
                _animationController.PlayAnimation(EEndGameCharacterAnimation.Excited);
            });

        });

        CoroutineRunner.Instance.WaitForSeconds(_stateDuration, () =>
        {
            FSM.SetTransition(ETransition.SecondWalk);
        });

    }
}