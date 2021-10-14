using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class CharacterFailState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Fail;
    }
}