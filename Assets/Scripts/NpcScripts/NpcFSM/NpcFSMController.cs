public class NpcFSMController : FSMController<NpcFSMController.EState,
    NpcFSMController.ETransition>
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        LowJoy = 2,
        MiddleJoy = 3,
        HighJoy = 4,
    }

    public enum ETransition
    {
        None = 0,
        Idle = 1,
        LowJoy = 2,
        MiddleJoy = 3,
        HighJoy = 4,
    }
}