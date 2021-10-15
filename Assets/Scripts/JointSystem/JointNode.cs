using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JointNode : JointBase
{
    private Sequence _jumpSequence = null;
    
    private IEnumerator _stickRoutine = null;

    private StickableJointSettings _settings;

    private StickableJointSettings _Settings
    {
        get
        {
            if (_settings == null)
                _settings = JointSettingsManager.Instance.JointSettings;

            return _settings;
        }
    }

    public override void StickToJoint(JointBase joint, Action onStuckCallback)
    {
        _jumpSequence?.Kill();

        Vector3 targetPosition = joint.JointAnchor.position;
        targetPosition.y += _Settings.StickableJumpHeightOffset;

        _jumpSequence = transform.DOJump(
            targetPosition,
            _Settings.StickableJumpPower,
            1,
            _Settings.StickableJumpDuration)
            .OnComplete(onJumpCompleted);

        void onJumpCompleted()
        {
            StartStickRoutine(joint, onStuckCallback);
        }
    }
    
    public void StartStickRoutine(JointBase joint, Action completedCallback)
    {
        StopStickProgress();

        _stickRoutine = StickProgress(joint, completedCallback);
        StartCoroutine(_stickRoutine);
    }

    private void StopStickProgress()
    {
        if (_stickRoutine != null)
            StopCoroutine(_stickRoutine);
    }

    private IEnumerator StickProgress(JointBase joint, Action completedCallback = null)
    {
        float curDist = Mathf.Infinity;

        float timePassed = 0f;
        
        do
        {
            timePassed += Time.deltaTime;
            
            transform.position = Vector3.Lerp(
                transform.position,
                joint.JointAnchor.position,
                timePassed/_Settings.StickDuration); ;

            yield return null;

        } while (timePassed < _Settings.StickDuration);

        transform.position = joint.JointAnchor.position;

        StuckToJoint(joint);

        completedCallback?.Invoke();
    }
}
