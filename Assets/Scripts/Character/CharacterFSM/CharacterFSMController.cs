public class CharacterFSMController : FSMController<CharacterFSMController.EState,
    CharacterFSMController.ETransition>
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        Run = 2,
        Fall = 3,
        Win = 4,
        Fail = 5,
        EndGame = 6
    }

    public enum ETransition
    {
        None = 0,
        Idle = 1,
        Run = 2,
        Fall = 3,
        Win = 4,
        Fail = 5,
        EndGame = 6
    }
}