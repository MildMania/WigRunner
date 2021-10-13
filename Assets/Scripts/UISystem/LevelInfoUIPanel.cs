using MMFramework.MMUI;
using UnityEngine;

public class LevelInfoUIPanel : VMBase, IUIPanel
{
    [SerializeField] private LevelIDWidget _levelIDWidget = null;
    [SubWidget] public LevelIDWidget LevelIDWidget => _levelIDWidget;

    protected override void AwakeCustomActions()
    {
        RegisterToPhases();

        base.AwakeCustomActions();
    }

    protected override void OnDestroyCustomActions()
    {
        UnregisterFromPhases();

        base.OnDestroyCustomActions();
    }

    private void RegisterToPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static += OnTraverseStarted;
    }

    private void UnregisterFromPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static -= OnTraverseStarted;
    }

    private void OnTraverseStarted(PhaseBaseNode phase)
    {
        if (phase is MainMenuPhase)
        {
            TryActivate();
        }

        if (phase is LevelWinPhase || phase is LevelFailPhase)
        {
            TryDeactivate();
        }
    }
}
