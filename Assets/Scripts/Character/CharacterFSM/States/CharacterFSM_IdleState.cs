using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFSM_IdleState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Idle;
    }
}