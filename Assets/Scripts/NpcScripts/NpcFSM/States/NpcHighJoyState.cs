using EState = NpcFSMController.EState;
using ETransition = NpcFSMController.ETransition;

public class NpcHighJoyState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.HighJoy;
    }
}