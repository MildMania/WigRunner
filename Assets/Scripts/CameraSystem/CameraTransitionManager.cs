using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
    private void Awake()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        PhaseBaseNode.OnTraverseStarted_Static += OnPhaseStarted;
    }

    private void UnregisterFromEvents()
    {
        PhaseBaseNode.OnTraverseStarted_Static -= OnPhaseStarted;
    }

    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (phase is GamePhase)
        {
            GameCameraTransition();
        }
        else if (phase is LevelWinPhase)
        {
            LevelWinPhaseCameraTransition();
        }
        else if (phase is LevelFailPhase)
        {
            LevelFailPhaseCameraTransition();
        }
    }

    private void GameCameraTransition()
    {
        CameraManager.Instance.ActivateCamera(new CameraActivationArgs(ECameraType.Game));
    }

    private void LevelWinPhaseCameraTransition()
    {
        CameraManager.Instance.ActivateCamera(new CameraActivationArgs(ECameraType.Win));
    }

    private void LevelFailPhaseCameraTransition()
    {
        CameraManager.Instance.ActivateCamera(new CameraActivationArgs(ECameraType.Fail));
    }
}
