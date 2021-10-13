using MMFramework.MMUI;
using MMFramework.TasksV2;
using MMFramework.Utilities;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class WinGameVM : VMBase
{
    [SerializeField] private ButtonWidget _nextLevelButtonWidget = null;
    [SubWidget] public ButtonWidget NextLevelButtonWidget => _nextLevelButtonWidget;

    [SerializeField] private MMTaskExecutor _winGameVMActivatedTaskExecutor = null;
    [SerializeField] private float _winGameVMAppearDelay = 3;

    private LevelWinPhase _levelWinPhase;

    [Binding]
    public void OnNextLevelButtonClicked()
    {
        TryDeactivate();

        _levelWinPhase.CompletePhase();
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
        if (phase is LevelWinPhase winPhase)
        {
            CoroutineRunner.Instance.WaitForSeconds(_winGameVMAppearDelay, () =>
            {
                _levelWinPhase = winPhase;

                TryActivate();

                _winGameVMActivatedTaskExecutor.Execute(this);
            });
        }
    }
}