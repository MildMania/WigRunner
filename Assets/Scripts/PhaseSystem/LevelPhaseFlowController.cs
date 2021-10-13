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
        LevelFailPhase levelFailPhase = new LevelFailPhase(42);
        LevelWinPhase levelWinPhase = new LevelWinPhase(41);
        CheckLevelEndPhase checkLevelEndPhase = new CheckLevelEndPhase(4, levelWinPhase, levelFailPhase);
        GamePhase gamePhase = new GamePhase(3);
        PreGamePhase preGamePhase = new PreGamePhase(2);
        MainMenuPhase mainMenuPhase = new MainMenuPhase(1);
        LevelPhase levelPhase = new LevelPhase(0, mainMenuPhase, preGamePhase, gamePhase, checkLevelEndPhase);


        TypeToPhaseNode.Add(levelFailPhase.GetType(), levelFailPhase);
        TypeToPhaseNode.Add(levelWinPhase.GetType(), levelWinPhase);
        TypeToPhaseNode.Add(checkLevelEndPhase.GetType(), checkLevelEndPhase);
        TypeToPhaseNode.Add(gamePhase.GetType(), gamePhase);
        TypeToPhaseNode.Add(preGamePhase.GetType(), preGamePhase);
        TypeToPhaseNode.Add(mainMenuPhase.GetType(), mainMenuPhase);
        TypeToPhaseNode.Add(levelPhase.GetType(), levelPhase);


        return levelPhase;
    }
}