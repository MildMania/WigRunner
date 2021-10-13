using MMFramework.MMUI;
using MMFramework.Utilities;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class FailGameVM : VMBase
{
    [SerializeField] private ButtonWidget _retryButtonWidget = null;
    [SubWidget] public ButtonWidget RetryButtonWidget => _retryButtonWidget;

    [SerializeField] private float _failGameVMAppearDelay = 3;

    private LevelFailPhase _levelFailPhase;

    [Binding]
    public void OnRetryButtonClicked()
    {
        TryDeactivate();

        _levelFailPhase.CompletePhase();
    }

    protected override void AwakeCustomActions()
    {
        RegisterToEvents();

        base.AwakeCustomActions();
    }

    protected override void OnDestroyCustomActions()
    {
        UnregisterFromEvents();

        base.OnDestroyCustomActions();
    }

    private void RegisterToEvents()
    {
        PhaseBaseNode.OnTraverseStarted_Static += OnPhaseStarted;
        PhaseBaseNode.OnTraverseFinished_Static += OnPhaseFinished;
    }

    private void UnregisterFromEvents()
    {
        PhaseBaseNode.OnTraverseStarted_Static -= OnPhaseStarted;
        PhaseBaseNode.OnTraverseFinished_Static -= OnPhaseFinished;
    }

    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (phase is LevelFailPhase failPhase)
        {
            CoroutineRunner.Instance.WaitForSeconds(_failGameVMAppearDelay, () =>
            {
                _levelFailPhase = failPhase;

                TryActivate();
            });
        }
    }

    private void OnPhaseFinished(PhaseBaseNode phase)
    {
        if (phase is LevelFailPhase)
        {
            TryDeactivate();
        }
    }
}