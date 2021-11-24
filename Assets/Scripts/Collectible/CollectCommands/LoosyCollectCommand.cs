using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "LoosyCollectCommand", menuName = "ScriptableObjects/LoosyCollectCommand",
    order = 1)]
public class LoosyCollectCommand : BaseCollectCommand
{
    [SerializeField] private Vector3 _distance;
    [SerializeField] public float _smoothTime = 0.1f;

    private float _velocity = 0;

    protected override void ExecuteCustomActions(Collectible collectible, Action onCollectCommandExecuted)
    {
        collectible.Collider.enabled = false;

        collectible.MoveRoutine = MoveRoutine(collectible);
        CoroutineRunner.Instance.StartCoroutine(collectible.MoveRoutine);

        onCollectCommandExecuted?.Invoke();
    }

    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {
    }


    private IEnumerator MoveRoutine(Collectible collectible)
    {
        int row = CollectedCollectibles.Count / TargetTransforms.Length;
        int column = CollectedCollectibles.Count % TargetTransforms.Length;

        TargetTransforms[column].Add(collectible.transform);
        var collectibleTransform = collectible.transform;
        collectibleTransform.parent = ParentTransform;


        while (PhaseTracker.Instance.CurrentPhase is GamePhase)
        {
            if (collectible == null) break;

            var collectiblePosition = collectibleTransform.position;
            Vector3 targetPosition = (TargetTransforms[column][row].position - _distance);
            collectibleTransform.position =
                new Vector3(Mathf.SmoothDamp(collectiblePosition.x,
                    targetPosition.x,
                    ref _velocity, _smoothTime), targetPosition.y, targetPosition.z);

            yield return null;
        }
    }
}