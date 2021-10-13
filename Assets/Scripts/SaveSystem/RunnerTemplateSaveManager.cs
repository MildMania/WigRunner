public class RunnerTemplateSaveManager : UserSaveManager

{
    protected override void OnPhaseTraverseStarted(PhaseBaseNode phase)
    {
        if (phase is LevelWinPhase)
        {
            int levelId = GameManager.Instance.GetVirtualLevelID();

            SaveCurLevel(levelId);
        }
    }
}