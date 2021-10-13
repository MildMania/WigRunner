using UnityEngine;

public class HapticController : MonoBehaviour
{
    private void Awake()
    {
        OnHapticRequestedEventRaiser.OnRaisedStatic += OnHapticRequested;
    }

    private void OnDestroy()
    {
        OnHapticRequestedEventRaiser.OnRaisedStatic -= OnHapticRequested;
    }

    private void OnHapticRequested(object sender, OnHapticRequestedEventArgs onHapticRequestedEventArgs)
    {
        iOSHapticFeedback.Instance.Trigger(onHapticRequestedEventArgs.FeedbackType);
    }
}