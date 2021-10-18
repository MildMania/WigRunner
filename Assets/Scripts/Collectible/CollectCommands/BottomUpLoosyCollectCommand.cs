using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BottomUpLoosyCollectCommand", menuName = "ScriptableObjects/BottomUpLoosyCollectCommand",
    order = 1)]
public class BottomUpLoosyCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpSpeed = 20f;

    protected override void ExecuteCustomActions(
        Collectible collectible, Action onCollectCommandExecuted)
    {
        collectible.Collider.enabled = false;
        CoroutineRunner.Instance.StartCoroutine(MoveRoutine(collectible));
        onCollectCommandExecuted?.Invoke();
    }

    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {
        ParentTransform = CollectedCollectibles.Count == 0
            ? CollectibleContainerTransform
            : CollectedCollectibles[CollectedCollectibles.Count - 1].transform;


        var bounds = collectible.Collider.bounds;
        TargetPosition =
            CollectibleContainerTransform.position + Vector3.up * CollectedCollectibles.Count * bounds.size.y;
    }

    private IEnumerator MoveRoutine(Collectible collectible)
    {
        var collectibleTransform = collectible.transform;

        while (PhaseTracker.Instance.CurrentPhase is GamePhase)
        {
            var collectiblePosition = collectibleTransform.position;


            Vector3 positionDifference = new Vector3(ParentTransform.position.x - collectiblePosition.x,
                TargetPosition.y - collectiblePosition.y,
                ParentTransform.position.z - collectiblePosition.z);

            collectiblePosition += Time.deltaTime * positionDifference * _lerpSpeed;
            collectibleTransform.position = collectiblePosition;

            yield return null;
        }
    }
}