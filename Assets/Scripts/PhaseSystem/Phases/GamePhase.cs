using ProgressionSystem;

public class GamePhase : PhaseActionNode
{
    public GamePhase(int id) : base(id)
    {
    }

    protected override void ProcessFlow()
    {
        UnityEngine.Debug.Log("Game Phase");
    }
}