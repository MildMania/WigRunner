using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FrontEndCollectCommand", menuName = "ScriptableObjects/FrontEndCollectCommand", order = 1)]
public class FrontEndCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpTime = 0.25f;

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
            CollectibleContainerTransform.position + Vector3.forward * CollectedCollectibles.Count * bounds.size.z;
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
            float step = currentTime / _lerpTime;

            Vector3 targetPosition =
                new Vector3(ParentTransform.position.x, ParentTransform.position.y, TargetPosition.z);
            collectibleTransform.position = Vector3.Lerp(position,
                targetPosition, step);

            collectibleTransform.rotation = Quaternion.Lerp(rotation,
                ParentTransform.rotation, step);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}