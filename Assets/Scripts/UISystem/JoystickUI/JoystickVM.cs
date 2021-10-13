using MVVM;

public class JoystickVM : VMBase<JoystickVM>
{
    protected override void RegisterActivationEvents()
    {
        RegisterToPhases();
    }

    protected override void UnregisterActivationEvents()
    {
        UnregisterFromPhases();
    }

    private void RegisterToPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static += OnPhaseTraverseStarted;
        PhaseBaseNode.OnTraverseFinished_Static += OnPhaseTraverseFinished;
    }

    private void UnregisterFromPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static -= OnPhaseTraverseStarted;
        PhaseBaseNode.OnTraverseFinished_Static -= OnPhaseTraverseFinished;
    }

    private void OnPhaseTraverseStarted(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
            return;
        
        NotifyPropertyChanged();

        ActivateUI();
    }

    private void OnPhaseTraverseFinished(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
            return;
        
        DeactivateUI();
    }
}
