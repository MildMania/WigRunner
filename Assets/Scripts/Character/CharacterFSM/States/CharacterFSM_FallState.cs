using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFSM_FallState : State<EState, ETransition>
{
    [SerializeField] private RagdollBehaviour _ragdollBehaviour = null;

    [SerializeField] private Vector3 _applyRagdollActivationForce = new Vector3(0, 5, 0);

    protected override EState GetStateID()
    {
        return EState.Fall;
    }

    public override void OnEnterCustomActions()
    {
        _ragdollBehaviour.SetRagdoll(true, _applyRagdollActivationForce);

        base.OnEnterCustomActions();
    }
}