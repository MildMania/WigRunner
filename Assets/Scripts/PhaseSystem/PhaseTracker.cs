using UnityEngine;

public class PhaseTracker : MonoBehaviour
{
    #region Singleton
    
    private static PhaseTracker _instance;
    public static PhaseTracker Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PhaseTracker>();

            return _instance;
        }
    }    
    
    #endregion
    
    public PhaseBaseNode CurPhase { get; private set; }
    
    private void Awake()
    {
        RegisterToPhases();
    }

    private void OnDestroy()
    {
        UnregisterFromPhases();
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
        CurPhase = phase;
    }
}
