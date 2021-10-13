using UnityEngine;
using System.Linq;
using System;
using System.Collections;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private ECameraType _initialCamera = ECameraType.None;

    #region Singleton
    private static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<CameraManager>();

            return _instance;
        }
    }
    #endregion

    private Camera _mainCamera;
    public Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;

            return _mainCamera;
        }
    }

    private VirtualCameraBase[] _virtualCameraList;
    private VirtualCameraBase[] _VirtualCameraList
    {
        get
        {
            if (_virtualCameraList == null)
                _virtualCameraList = FindObjectsOfType<VirtualCameraBase>();

            return _virtualCameraList;
        }
    }

    private CinemachineBlenderSettings _blenderSettings;
    private CinemachineBlenderSettings _BlenderSettings
    {
        get
        {
            if (_blenderSettings == null)
                _blenderSettings = Camera.main.GetComponent<CinemachineBrain>().m_CustomBlends;

            return _blenderSettings;
        }
    }

    public ECameraType CurCameraType { get; private set; }

    private VirtualCameraBase _curCamera;

    private IEnumerator _blendRoutine;

    private const string _anyCamera = "**ANY CAMERA**";

    #region Events
    public Action<ECameraType> OnCameraBlendStarted { get; set; }
    public Action<ECameraType> OnCameraBlendFinished { get; set; }
    #endregion

    private void Awake()
    {
        InitializeCameras();
        DeactivateSecondaryCameras();
        ActivateCamera(new CameraActivationArgs(_initialCamera));
        SetCameraTypes();
    }

    private void InitializeCameras()
    {
        foreach (VirtualCameraBase cam in _VirtualCameraList)
            cam.Initialize();
    }

    public void ActivateCamera(CameraActivationArgs e)
    {
        if (CurCameraType == e.CameraType)
        {
            // UpdateCameraFollowAndLookAt(e);

            OnCameraBlendFinished?.Invoke(CurCameraType);

            return;
        }

        VirtualCameraBase vCam = _VirtualCameraList
            .SingleOrDefault(val => val.CameraType == e.CameraType);

        VirtualCameraBase prevCamera = GetCamera(CurCameraType);

        CurCameraType = vCam.CameraType;
        _curCamera = vCam;

        DeactivateSecondaryCameras();

        vCam.Activate();

        // UpdateCameraFollowAndLookAt(e);

        OnCameraBlendStarted?.Invoke(CurCameraType);

        if (prevCamera != null)
            CheckCameraBlendCompletion(prevCamera, vCam);
    }

    private void UpdateCameraFollowAndLookAt(CameraActivationArgs e)
    {
        UpdateCameraLookAt(e.LookAt);
        UpdateCameraTarget(e.Target);
    }

    private void UpdateCameraTarget(Transform target)
    {
        //if (target == null)
        //    return;

        _curCamera.VirtualCamera.Follow = target;
    }

    private void UpdateCameraLookAt(Transform lookAt)
    {
        //if (lookAt == null)
        //    return;

        _curCamera.VirtualCamera.LookAt = lookAt;
    }

    private void CheckCameraBlendCompletion(VirtualCameraBase from, VirtualCameraBase to)
    {
        StartBlendRoutine(from, to);
    }

    private void StartBlendRoutine(VirtualCameraBase from, VirtualCameraBase to)
    {
        StopBlendRoutine();

        _blendRoutine = BlendProgress(from, to);

        StartCoroutine(_blendRoutine);
    }

    private void StopBlendRoutine()
    {
        if (_blendRoutine != null)
            StopCoroutine(_blendRoutine);
    }

    private IEnumerator BlendProgress(VirtualCameraBase from, VirtualCameraBase to)
    {
        float _remBlendTime = GetBlendDuration(from, to);

        while (_remBlendTime > 0)
        {
            _remBlendTime -= Time.deltaTime;

            yield return null;
        }

        OnCameraBlendFinished?.Invoke(CurCameraType);
    }

    private void SetCameraTypes()
    {
        CurCameraType = _VirtualCameraList.SingleOrDefault(val => val.CameraType == _initialCamera).CameraType;
    }

    private void DeactivateSecondaryCameras()
    {
        foreach (VirtualCameraBase vCam in _VirtualCameraList)
            if (vCam.CameraType != CurCameraType)
                vCam.Deactivate();
    }

    public VirtualCameraBase GetCamera(ECameraType cameraType)
    {
        return _VirtualCameraList.SingleOrDefault(val => val.CameraType == cameraType);
    }

    private float GetBlendDuration(VirtualCameraBase fromCamera, VirtualCameraBase toCamera)
    {
        CinemachineBlenderSettings.CustomBlend blend = _BlenderSettings.m_CustomBlends.FirstOrDefault(
                    val => val.m_From == fromCamera.CameraName
                    && val.m_To == toCamera.CameraName);

        if (blend.m_From != null)
            return blend.m_Blend.m_Time;

        blend = _BlenderSettings.m_CustomBlends.FirstOrDefault(
                    val => val.m_From == fromCamera.CameraName
                    && val.m_To == _anyCamera);

        if (blend.m_From != null)
            return blend.m_Blend.m_Time;

        blend = _BlenderSettings.m_CustomBlends.FirstOrDefault(
                   val => val.m_From == _anyCamera
                   && val.m_To == toCamera.CameraName);

        if (blend.m_From != null)
            return blend.m_Blend.m_Time;

        return _BlenderSettings.m_CustomBlends.First(
            val => val.m_From == _anyCamera
                    && val.m_To == _anyCamera).m_Blend.m_Time;
    }
}
