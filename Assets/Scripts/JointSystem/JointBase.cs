using UnityEngine;
using System;
using JointSystem;

public abstract class JointBase : MonoBehaviour
{
    private Transform _jointAnchor;

    public Transform JointAnchor
    {
        get
        {
            if (_jointAnchor == null)
            {
                JointAnchor ja = GetComponentInChildren<JointAnchor>();
                if (ja != null)
                {
                    _jointAnchor = GetComponentInChildren<JointAnchor>().transform;
                }
            }

            return _jointAnchor;
        }
    }

    public GameObject ZRotObj;


    public JointBase ConnectedJoint { get; protected set; }
    Rigidbody _rigidbody;

    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            return _rigidbody;
        }
    }

    HingeJoint _hingeJoint;

    public Action<JointBase, JointBase> OnStuckToJoint { get; set; }
    public Action<JointBase> OnDeattachedFromJoint { get; set; }


    public abstract void StickToJoint(JointBase joint, Action onStuckCallback = null);

    protected void StuckToJoint(JointBase joint)
    {
        ConnectedJoint = joint;

        _hingeJoint = ZRotObj.AddComponent<HingeJoint>();
        _hingeJoint.anchor = new Vector3(0, -0.5f, 0);
        _hingeJoint.axis = new Vector3(0, 0, 1);

        _hingeJoint.useSpring = true;
        _hingeJoint.spring = new JointSpring()
        {
            spring = JointSettingsManager.Instance.JointSettings.JointSpring,
        };

        _hingeJoint.useLimits = true;
        _hingeJoint.limits = new JointLimits()
        {
            min = -JointSettingsManager.Instance.JointSettings.JointLimitAngle,
            max = JointSettingsManager.Instance.JointSettings.JointLimitAngle
        };

        _hingeJoint.massScale = JointSettingsManager.Instance.JointSettings.MassScale;

        _hingeJoint.connectedBody = joint.Rigidbody;

        OnStuckToJoint?.Invoke(this, joint);
    }

    public void DettachFromJoint()
    {
        Destroy(_hingeJoint);

        OnDeattachedFromJoint?.Invoke(this);
    }
}