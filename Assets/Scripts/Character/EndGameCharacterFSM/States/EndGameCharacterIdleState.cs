using EState = EndGameCharacterFSMController.EState;
using ETransition = EndGameCharacterFSMController.ETransition;

public class EndGameCharacterIdleState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Idle;
    }
}