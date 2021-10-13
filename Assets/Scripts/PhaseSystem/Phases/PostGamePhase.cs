public class PostGamePhase : PhaseActionNode
{   
    public PostGamePhase(int id) : base(id)
    {
    }

    protected override void ProcessFlow()
    {
        UnityEngine.Debug.Log("Post Game Phase");

        TraverseCompleted();
    }
}
