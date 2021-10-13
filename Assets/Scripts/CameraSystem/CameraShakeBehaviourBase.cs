using Cinemachine;
using UnityEngine;

public abstract class CameraShakeBehaviourBase : MonoBehaviour, ICameraShakeBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _shakeCamera = null;

    private CinemachineBasicMultiChannelPerlin _channelPerlin;
    protected CinemachineBasicMultiChannelPerlin _ChannelPerlin
    {
        get
        {
            if (_channelPerlin == null)
                _channelPerlin = _shakeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            return _channelPerlin;
        }
    }

    public abstract void ActivateShake(CameraShakeArgs shakeArgs);

    public abstract void DeactivateShake();
}
