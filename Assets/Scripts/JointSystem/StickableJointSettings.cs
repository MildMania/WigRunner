using UnityEngine;

[CreateAssetMenu(fileName = "StickableJointSettings", menuName = "Joint Settings/Create Joint Settings", order = 1)]
public class StickableJointSettings : ScriptableObject
{
    [Header("Jump Settings")]
    [SerializeField] private float _stickableJumpDuration = 0.2f;
    public float StickableJumpDuration => _stickableJumpDuration;
    
    [SerializeField] private float _stickableJumpPower = 1f;
    public float StickableJumpPower => _stickableJumpPower;
    
    [SerializeField] private float _stickableJumpHeightOffset = 1f;
    public float StickableJumpHeightOffset => _stickableJumpHeightOffset;

    [Header("Stick Settings")]
    [SerializeField] private float _stickDistance = 0.4f;
    public float StickDistance => _stickDistance;
    
    [SerializeField] private float _stickDuration = 0.2f;
    public float StickDuration => _stickDuration;

    [Header("Joint Settings")]
    [SerializeField] private float _jointLimitAngle = 0.2f;
    public float JointLimitAngle => _jointLimitAngle;
    
    [SerializeField] private float _jointSpring = 100;
    public float JointSpring => _jointSpring;
    
    [SerializeField] private float _massScale = 10;
    public float MassScale => _massScale;
}
