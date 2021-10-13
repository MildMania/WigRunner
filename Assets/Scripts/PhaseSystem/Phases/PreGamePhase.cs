using UnityEngine;

public class PreGamePhase : PhaseActionNode
{
    public PreGamePhase(int id) : base(id)
    {
    }

    protected override void ProcessFlow()
    {
        Debug.Log("Pre Game Phase");
    }
}