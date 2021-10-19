using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "FrontEndCollectCommand", menuName = "ScriptableObjects/FrontEndCollectCommand", order = 1)]
public class FrontEndCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpTime = 0.25f;
    private Bounds _bounds;


    private ParentConstraint _parentConstraint;

    private bool _isParentConstraintSet = false;

    protected override void ExecuteCustomActions(
        Collectible collectible, Action onCollectCommandExecuted)
    {
        _bounds = collectible.Collider.bounds;
        collectible.Collider.enabled = false;
        CoroutineRunner.Instance.StartCoroutine(MoveRoutine(collectible));
        onCollectCommandExecuted?.Invoke();
    }

    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {
        TargetTransform = CollectedCollectibles.Count == 0
            ? TargetTransform
            : CollectedCollectibles[CollectedCollectibles.Count - 1].transform;

        if (CollectedCollectibles.Count == 0)
        {
            _parentConstraint = ParentTransform.gameObject.AddComponent<ParentConstraint>();
            ConstraintSource constraintSource = new ConstraintSource();
            constraintSource.sourceTransform = TargetTransform;
            constraintSource.weight = 1;
            _parentConstraint.AddSource(constraintSource);
        }
    }


    private IEnumerator MoveRoutine(Collectible collectible)
    {
        float currentTime = 0;

        var collectibleTransform = collectible.transform;
        collectibleTransform.parent = ParentTransform;

        Quaternion rotation = collectibleTransform.rotation;
        Vector3 position = collectibleTransform.position;


        while (currentTime < _lerpTime)
        {
            if (!_isParentConstraintSet && _parentConstraint != null)
            {
                _isParentConstraintSet = true;
                _parentConstraint.constraintActive = true;
                ParentTransform.position = Vector3.zero;
            }

            float step = currentTime / _lerpTime;

            Vector3 targetPosition =
                new Vector3(TargetTransform.position.x, TargetTransform.position.y,
                    TargetTransform.position.z + _bounds.size.z);
            collectibleTransform.position = Vector3.Lerp(position,
                targetPosition, step);

            collectibleTransform.rotation = Quaternion.Lerp(rotation,
                TargetTransform.rotation, step);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}