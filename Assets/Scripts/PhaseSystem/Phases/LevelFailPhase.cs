public class LevelFailPhase : PhaseActionNode
{
    public LevelFailPhase(int id) : base(id)
    {
    }

    protected override void ProcessFlow()
    {
        UnityEngine.Debug.Log("Level Fail Phase");
    }

    public void CompletePhase()
    {
        TraverseCompleted();

        GameManager.Instance.SceneManager.LoadCurScene();
    }
}
