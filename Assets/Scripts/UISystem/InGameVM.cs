using MMFramework.MMUI;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class InGameVM : VMBase
{
    [SerializeField] private FillBarWidget _progressionFillBarWidget = null;
    [SubWidget] public FillBarWidget ProgressionFillBarWidget => _progressionFillBarWidget;

    private string _progressionString;

    [Binding]
    public string ProgressionString
    {
        get => _progressionString;
        set
        {
            _progressionString = value;
            OnPropertyChanged(nameof(ProgressionString));
        }
    }

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
        PhaseBaseNode.OnTraverseStarted_Static += OnPhaseStarted;
    }

    private void UnregisterFromPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static -= OnPhaseStarted;
    }

    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (phase is GamePhase)
        {
            TryActivate();
        }

        if (phase is CheckLevelEndPhase)
        {
            TryDeactivate();
        }
    }
}