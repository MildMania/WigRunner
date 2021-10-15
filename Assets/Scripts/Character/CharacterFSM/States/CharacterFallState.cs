using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFallState : State<EState, ETransition>
{
    [SerializeField] private TriggerObjectHitController _fallTriggerHitController;

    protected override EState GetStateID()
    {
        return EState.Fall;
    }

    private void Awake()
    {
        _fallTriggerHitController.OnHitTriggerObject += OnHitTriggerObject;
    }

    private void OnDestroy()
    {
        _fallTriggerHitController.OnHitTriggerObject -= OnHitTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject obj)
    {
        FSM.SetTransition(ETransition.Fail);
    }
}