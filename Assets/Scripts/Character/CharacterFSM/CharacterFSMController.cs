public class CharacterFSMController : FSMController<CharacterFSMController.EState, 
    CharacterFSMController.ETransition>
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        Run = 2,
        Walk = 3,
        ShowOffWin = 4,
        Fall = 5,
        Catwalk = 6,
        ShowOffFail = 7,
        Stop = 8,
    }

    public enum ETransition
    {
        None = 0,
        Idle = 1,
        Run = 2,
        Walk = 3,
        ShowOffWin = 4,
        Fall = 5,
        Catwalk = 6,
        ShowOffFail = 7,
        Stop = 8
    }
}