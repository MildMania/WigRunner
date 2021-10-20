using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BottomUpLoosyCollectCommand", menuName = "ScriptableObjects/BottomUpLoosyCollectCommand",
    order = 1)]
public class BottomUpLoosyCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpSpeed = 20f;
    private Bounds _bounds;

    protected override void ExecuteCustomActions(
        Collectible collectible, Action onCollectCommandExecuted)
    {
        _bounds = collectible.Collider.bounds;

        collectible.Collider.enabled = false;

        collectible.MoveRoutine = MoveRoutine(collectible);
        CoroutineRunner.Instance.StartCoroutine(collectible.MoveRoutine);

        onCollectCommandExecuted?.Invoke();
    }

    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {
        TargetTransform = CollectedCollectibles.Count == 0
            ? TargetTransform
            : CollectedCollectibles[CollectedCollectibles.Count - 1].transform;
    }

    private IEnumerator MoveRoutine(Collectible collectible)
    {
        var collectibleTransform = collectible.transform;
        collectibleTransform.parent = ParentTransform;
        while (PhaseTracker.Instance.CurrentPhase is GamePhase)
        {
            if (collectible == null) break;

            var collectiblePosition = collectibleTransform.position;


            Vector3 positionDifference = new Vector3(TargetTransform.position.x - collectiblePosition.x,
                TargetTransform.position.y + _bounds.size.y - collectiblePosition.y,
                TargetTransform.position.z - collectiblePosition.z);

            collectiblePosition += Time.deltaTime * positionDifference * _lerpSpeed;
            collectibleTransform.position = collectiblePosition;

            yield return null;
        }
    }
}