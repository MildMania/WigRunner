using System.Collections;
using UnityEngine;

public class CameraImpactShakeBehaviour : CameraShakeBehaviourBase
{
    private IEnumerator _impactShakeRoutine;

    public override void ActivateShake(CameraShakeArgs shakeArgs)
    {
        DeactivateShake();

        _impactShakeRoutine = ImpactShakeProgress((CameraImpactShakeArgs)shakeArgs);

        StartCoroutine(_impactShakeRoutine);
    }

    public override void DeactivateShake()
    {
        if (_impactShakeRoutine != null)
            StopCoroutine(_impactShakeRoutine);
    }

    private IEnumerator ImpactShakeProgress(CameraImpactShakeArgs shakeArgs)
    {
        _ChannelPerlin.m_AmplitudeGain = shakeArgs.AmplitudePeak;

        float fadeDuration = shakeArgs.FadeDuration;

        float passedTime = 0;

        while (_ChannelPerlin.m_AmplitudeGain > 0)
        {
            _ChannelPerlin.m_AmplitudeGain = Mathf.Lerp(shakeArgs.AmplitudePeak, 0, passedTime / fadeDuration);

            passedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        _ChannelPerlin.m_AmplitudeGain = 0;
    }
}
