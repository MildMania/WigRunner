using System;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;
using MMUtils = MMFramework.Utilities.Utilities;

public class CharacterRunState : State<EState, ETransition>
{
    [SerializeField] private CharacterAnimationController _characterAnimationController = null;
    [SerializeField] private CharacterInputController _characterInputController = null;
    [SerializeField] private CharacterMovementBehaviour _characterMovementBehaviour = null;
    [SerializeField] private Transform _characterTransform = null;
    [SerializeField] private float _zSpeed = 2.0f;
    [SerializeField] private float _xSpeed = 5f;
    [SerializeField] private float _ySpeed = -1f;

    private float _xSwipeAmount = 0;
    private float _platformWidth;

    #region Events

    public Action<Vector3> OnCharacterMoved { get; set; }

    #endregion

    protected override EState GetStateID()
    {
        return EState.Run;
    }

    public override void OnEnterCustomActions()
    {
        _characterAnimationController.PlayAnimation(ECharacterAnimation.Run);

        base.OnEnterCustomActions();
    }

    private void Awake()
    {
        _platformWidth = Math.Abs(LevelBoundaryProvider.Instance.GetLeftBoundary().x -
                                  LevelBoundaryProvider.Instance.GetRightBoundary().x);
        SubscribeToEvents();
    }

    private void Update()
    {
        TryMove();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    [PhaseListener(typeof(GamePhase), true)]
    private void SubscribeToEvents()
    {
        RegisterToInputController();
    }

    [PhaseListener(typeof(GamePhase), false)]
    private void UnsubscribeFromEvents()
    {
        UnregisterFromInputController();
    }


    #region Sideways Input

    private void RegisterToInputController()
    {
        _characterInputController.OnCharacterInputStarted += OnInputStarted;
        _characterInputController.OnCharacterInputPerformed += OnInputPerformed;
        _characterInputController.OnCharacterInputCancelled += OnInputCancelled;
    }

    private void UnregisterFromInputController()
    {
        _characterInputController.OnCharacterInputStarted -= OnInputStarted;
        _characterInputController.OnCharacterInputPerformed -= OnInputPerformed;
        _characterInputController.OnCharacterInputCancelled -= OnInputCancelled;
    }

    private void OnInputStarted(Vector2 delta)
    {
        FSM.SetTransition(ETransition.Run);
    }

    private void OnInputPerformed(Vector2 delta)
    {
        if (!FSM.GetCurStateID().Equals(GetStateID()))
        {
            FSM.SetTransition(ETransition.Run);
        }

        _xSwipeAmount += delta.x * _platformWidth;

        _xSwipeAmount = Mathf.Clamp(_xSwipeAmount,
            LevelBoundaryProvider.Instance.GetLeftBoundary().x,
            LevelBoundaryProvider.Instance.GetRightBoundary().x);
    }

    private void OnInputCancelled(Vector2 delta)
    {
    }

    #endregion

    private bool TryMove()
    {
        if (!CanMove())
        {
            return false;
        }

        var characterPosition = _characterTransform.position;
        Vector3 sideWayDir = _characterTransform.right * (_xSwipeAmount - characterPosition.x);

        _characterMovementBehaviour.Move(_characterTransform.forward * _zSpeed +
                                         sideWayDir * _xSpeed +
                                         new Vector3(0, _ySpeed, 0) * Time.deltaTime);

        return true;
    }

    private bool CanMove()
    {
        return FSM.GetCurStateID().Equals(GetStateID());
    }
}