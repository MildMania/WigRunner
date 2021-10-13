using ProgressionSystem;

public class GamePhase : PhaseActionNode
{
    public GamePhase(int id) : base(id)
    {
    }

    protected override void ProcessFlow()
    {
        UnityEngine.Debug.Log("Game Phase");

        ProgressionManager.Instance.OnProgressionUpdated += OnProgressionUpdated;
    }

    private void OnProgressionUpdated(EProgressionResult progressionResult)
    {
        if (progressionResult == EProgressionResult.None)
        {
            return;
        }

        ProgressionManager.Instance.OnProgressionUpdated -= OnProgressionUpdated;

        TraverseCompleted();
    }
}