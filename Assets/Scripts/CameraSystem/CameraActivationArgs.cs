using System;
using UnityEngine;

public class CameraActivationArgs : EventArgs
{
    public ECameraType CameraType { get; private set; }
    public Transform Target { get; private set; }
    public Transform LookAt { get; private set; }

    public CameraActivationArgs(
        ECameraType cameraType,
        Transform target = null,
        Transform lookAt = null)
    {
        CameraType = cameraType;
        Target = target;
        LookAt = lookAt;
    }
}

