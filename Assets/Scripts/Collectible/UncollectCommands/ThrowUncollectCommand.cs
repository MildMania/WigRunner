using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ThrowUncollectCommand",
    menuName = "ScriptableObjects/Uncollect/ThrowUncollectCommand",
    order = 1)]
public class ThrowUncollectCommand : BaseUncollectCommand
{
    protected override void ExecuteCustomActions(Collectible collectible,
        Action onUncollectCommandExecuted)
    {
        var collectibleGO = collectible.gameObject;
        collectibleGO.transform.parent = null;

        var collectibleCollider = collectibleGO.GetComponent<Collider>();
        collectibleCollider.enabled = true;
        collectibleCollider.isTrigger = false;

        var collectibleRigidbody = collectibleGO.GetComponent<Rigidbody>();
        collectibleRigidbody.isKinematic = false;
        collectibleRigidbody.useGravity = true;

        // Temporary
        var throwDirection = Vector3.up +
                             Vector3.right * Random.Range(-1f, 1f) +
                             Vector3.forward * Random.Range(-1f, 1f);

        collectibleRigidbody.AddForce(throwDirection * 300);
        onUncollectCommandExecuted?.Invoke();
    }
}