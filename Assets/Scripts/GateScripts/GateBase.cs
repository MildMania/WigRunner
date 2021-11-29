using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBase : MonoBehaviour
{

    public virtual void OnEnteredGate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnteredGate();
    }
}
