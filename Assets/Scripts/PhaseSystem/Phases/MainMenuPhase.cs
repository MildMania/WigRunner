public class MainMenuPhase : PhaseActionNode
{
    public MainMenuPhase(int id) : base(id)
    {
    }

    protected override void ProcessFlow()
    {
        UnityEngine.Debug.Log("Main Menu Phase");
    }

    public void CompletePhase()
    {
        TraverseCompleted();
    }
}