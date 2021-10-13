using System;

public class OnHapticRequestedEventArgs : EventArgs
{
    public iOSHapticFeedback.iOSFeedbackType FeedbackType { get; private set; }

    public OnHapticRequestedEventArgs(iOSHapticFeedback.iOSFeedbackType feedbackType)
    {
        FeedbackType = feedbackType;
    }
}
public class OnHapticRequestedEventRaiser : EventRaiserBase<OnHapticRequestedEventArgs>
{
}
