using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ThrowUncollectCommand",
                 menuName = "ScriptableObjects/Uncollect/ThrowUncollectCommand",
                 order = 1)]
public class ThrowUncollectCommand : BaseUncollectCommand
{
    protected override void ExecuteCustomActions(Collectible collectible,
                                                 Action onCollectCommandExecuted)
    {
        if (collectible.MoveRoutine != null)
        {
            CoroutineRunner.Instance.StopCoroutine(collectible.MoveRoutine);
        }

        var collectibleGO = collectible.gameObject;

        var collectibleCollider = collectibleGO.GetComponent<Collider>();
        collectibleCollider.enabled = true;
        collectibleCollider.isTrigger = false;

        var collectibleRB = collectibleGO.GetComponent<Rigidbody>();
        collectibleRB.isKinematic = false;
        collectibleRB.useGravity = true;

        // Temporary
        var throwDirection = Vector3.up +
                             Vector3.right * Random.Range(-1f, 1f) +
                             Vector3.forward * Random.Range(-1f, 1f);

        collectibleRB.AddForce(throwDirection * 300);
    }
}