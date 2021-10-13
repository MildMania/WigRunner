using UnityEngine;
using Cinemachine;

public class VirtualCameraBase : MonoBehaviour
{
    [SerializeField] private ECameraType _cameraType = ECameraType.None;

    public ECameraType CameraType
    {
        get
        {
            return _cameraType;
        }
    }

    public string CameraName
    {
        get
        {
            return gameObject.name;
        }
    }

    private CinemachineVirtualCamera _virtualCamera;
    public CinemachineVirtualCamera VirtualCamera
    {
        get
        {
            if (_virtualCamera == null)
                _virtualCamera = GetComponent<CinemachineVirtualCamera>();

            return _virtualCamera;
        }
    }

    public bool IsActive { get; private set; }

    public virtual void Initialize()
    {
    }

    public void Activate()
    {
        VirtualCamera.Priority = 1;

        ActivateCustomActions();

        IsActive = true;
    }

    protected virtual void ActivateCustomActions()
    {

    }

    public void Deactivate()
    {
        VirtualCamera.Priority = 0;

        DeactivateCustomActions();

        IsActive = false;
    }

    protected virtual void DeactivateCustomActions()
    {

    }

    private void OnDestroy()
    {
        Deactivate();
    }
}
