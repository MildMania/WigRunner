public class CameraImpactShakeArgs : CameraShakeArgs
{
    public float AmplitudePeak { get; }
    public float FadeDuration { get; }

    public CameraImpactShakeArgs(float amplitudePeak, float fadeDuration)
    {
        AmplitudePeak = amplitudePeak;

        FadeDuration = fadeDuration;
    }
}
