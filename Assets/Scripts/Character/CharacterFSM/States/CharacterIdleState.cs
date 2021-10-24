using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterIdleState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Idle;
    }
}