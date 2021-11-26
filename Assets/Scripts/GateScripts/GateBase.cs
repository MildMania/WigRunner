using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBase : MonoBehaviour
{
    [SerializeField] protected Collider _collider;

    public virtual void OnEnteredGate(Collectible collectible)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var collectible = other.gameObject.GetComponent<Collectible>();

        if (collectible)
            OnEnteredGate(collectible);
    }
}
