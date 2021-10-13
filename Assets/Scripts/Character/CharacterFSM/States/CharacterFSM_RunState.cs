using System;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;
using MMUtils = MMFramework.Utilities.Utilities;

public class CharacterFSM_RunState : State<EState, ETransition>
{
    [SerializeField] private CharacterAnimationController _characterAnimationController = null;

    [SerializeField] private CharacterInputController _characterInputController = null;
    
    [SerializeField] private CharacterMovementBehaviour _characterMovementBehaviour = null;

    [SerializeField] private Transform _characterTransform = null;

    [SerializeField] private Transform _forwardSpeedProviderObj = null;
    
    private ISpeedProvider _forwardSpeedProvider;
    private ISpeedProvider _ForwardSpeedProvider
    {
        get
        {
            if (_forwardSpeedProvider == null)
            {
                _forwardSpeedProvider = _forwardSpeedProviderObj.GetComponent<ISpeedProvider>();
            }

            return _forwardSpeedProvider;
        }
    }

    [SerializeField] private float _sidewaysMoveSpeed = 5f;

    [SerializeField] private float _sidewaysDeltaMultiplier = 2f;

    [SerializeField] private float _additionalYSpeedAmount = -1f;

    private float _curSidewaysMoveSliderVal = 0;

    #region Events

    public Action<Vector3> OnCharacterMoved { get; set; }

    #endregion
    
    protected override EState GetStateID()
    {
        return EState.Run;
    }

    public override void OnEnterCustomActions()
    {
        _characterAnimationController.PlayAnimation(ECharacterAnimation.Walk);
        
        base.OnEnterCustomActions();
    }

    protected override void OnExitCustomActions()
    {
        base.OnExitCustomActions();
    }

    private void Awake()
    {
        RegisterToPhases();
    }

    private void Update()
    {
        TryMove();
    }

    private void OnDestroy()
    {
        UnregisterFromPhases();
    }
    
    private void RegisterToPhases()
    {
        PhaseActionNode.OnTraverseStarted_Static += OnPhaseStarted;
        PhaseActionNode.OnTraverseFinished_Static += OnPhaseFinsihed;
    }

    private void UnregisterFromPhases()
    {
        PhaseActionNode.OnTraverseStarted_Static -= OnPhaseStarted;
        PhaseActionNode.OnTraverseFinished_Static -= OnPhaseFinsihed;
    }

    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
            return;

        RegisterToInputController();

        // FSM.SetTransition(ETransition.Run);
    }
    
    private void OnPhaseFinsihed(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
            return;

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
        
        _curSidewaysMoveSliderVal += delta.x * _sidewaysDeltaMultiplier;

        _curSidewaysMoveSliderVal = Mathf.Clamp(_curSidewaysMoveSliderVal, 
            LevelBoundsProvider.Instance.GetMinBoundsPos().x, 
            LevelBoundsProvider.Instance.GetMaxBoundsPos().x);
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

        Vector3 sideWayDir = _characterTransform.right * (_curSidewaysMoveSliderVal - _characterTransform.position.x);

        _characterMovementBehaviour.Move(_characterTransform.forward * _ForwardSpeedProvider.CalculateSpeed() +
                                         sideWayDir * _sidewaysMoveSpeed + new Vector3(0, _additionalYSpeedAmount, 0));

        OnCharacterMoved?.Invoke(_characterTransform.position);

        return true;
    }
    
    private bool CanMove()
    {
        return FSM.GetCurStateID().Equals(GetStateID());
    }

}