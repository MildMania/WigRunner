public class LevelPhaseFlowController : PhaseFlowController
{
    public LevelPhaseFlowController(int levelID)
        : base()
    {
        LevelPhase levelPhase = (LevelPhase)TreeRootNode;

        levelPhase.LevelID = levelID;
    }

    protected override PhaseBaseNode CreateRootNode()
    {
        return new LevelPhase(
            0,
            new PreGamePhase(1),
            new GamePhase(2),
            new CheckLevelEndPhase(3,
                new LevelWinPhase(31),
                new LevelFailPhase(32))
        );
    }
}