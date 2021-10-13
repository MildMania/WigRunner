public class BeautyFascinatorPhaseFlowManager : PhaseFlowManager
{
    protected override PhaseFlowController CreatePhase()
    {
        return new LevelPhaseFlowController(GameManager.Instance.GetCurLevelID());
    }
}
