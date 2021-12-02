public class EndGameCharacterFSMController : FSMController<EndGameCharacterFSMController.EState,
    EndGameCharacterFSMController.ETransition>
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        FirstWalk = 2,
        ObtainWig = 3,
        SecondWalk = 4,
        Posing = 5
    }

    public enum ETransition
    {
        None = 0,
        Idle = 1,
        FirstWalk = 2,
        ObtainWig = 3,
        SecondWalk = 4,
        Posing = 5
    }
}