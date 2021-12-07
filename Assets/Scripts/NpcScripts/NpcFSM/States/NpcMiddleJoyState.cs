using EState = NpcFSMController.EState;
using ETransition = NpcFSMController.ETransition;

public class NpcMiddleJoyState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.MiddleJoy;
    }
}