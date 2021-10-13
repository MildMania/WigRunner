using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour,
    IInputReceiver
{
    public List<InputTransmitter> AttachedInputTransmitterList { get; set; }
    public Dictionary<Type, InputTransmitter.EventDelegate> Delegates { get; set; }
    public Dictionary<Delegate, InputTransmitter.EventDelegate> DelegateLookUp { get; set; }

    private Vector2 _lastFingerPosition { get; set; } = new Vector2(int.MinValue, int.MinValue);
    
    public Action<Vector2> OnCharacterInputStarted { get; set; }
    public Action<Vector2> OnCharacterInputCancelled { get; set; }
    public Action<Vector2> OnCharacterInputPerformed { get; set; }
    
    private void Awake()
    {
        RegisterToPhaseEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromPhaseEvents();
    }

    private void RegisterToPhaseEvents()
    {
        PhaseActionNode.OnTraverseStarted_Static += OnPhaseStarted;
        PhaseActionNode.OnTraverseFinished_Static += OnPhaseFinished;
    }

    private void UnregisterFromPhaseEvents()
    {
        PhaseActionNode.OnTraverseStarted_Static -= OnPhaseStarted;
        PhaseActionNode.OnTraverseFinished_Static -= OnPhaseFinished;
    }

    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
            return;
        
        RegisterToInputReceiver();
    }

    private void OnPhaseFinished(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
            return;
        
        UnregisterFromInputReceiver();
    }

    private void RegisterToInputReceiver()
    {
        this.AddInputListener<Input_WI_OnFingerDown>(OnFingerDown);
        this.AddInputListener<Input_WI_OnFingerUp>(OnFingerUp);
        this.AddInputListener<Input_WI_OnPress>(OnPress);
    }

    private void UnregisterFromInputReceiver()
    {
        this.RemoveInputListener<Input_WI_OnFingerDown>(OnFingerDown);
        this.RemoveInputListener<Input_WI_OnFingerUp>(OnFingerUp);
        this.RemoveInputListener<Input_WI_OnPress>(OnPress);
    }

    private void OnFingerDown(Input_WI_OnFingerDown e)
    {
        _lastFingerPosition = e.FingerPos;
        
        OnCharacterInputStarted?.Invoke(Vector2.zero);
    }

    private void OnFingerUp(Input_WI_OnFingerUp e)
    {
        _lastFingerPosition = Vector2.zero;
        
        OnCharacterInputCancelled?.Invoke(_lastFingerPosition);
    }

    private void OnPress(Input_WI_OnPress e)
    {
        if (_lastFingerPosition == Vector2.one * int.MinValue)
        {
            _lastFingerPosition = e.FingerPos;
            
            return;
        }

        Vector2 deltaMovement = e.FingerPos - _lastFingerPosition;

        float normalized = deltaMovement.x / Screen.width;
        
        OnCharacterInputPerformed?.Invoke(new Vector2(normalized, 0));

        _lastFingerPosition = e.FingerPos;
    }
}
