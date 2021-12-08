using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrowdGate : GateBase
{
    [SerializeField] private CrowdController _crowdController;

    public override void OnEnteredGate()
    {
        if (_isEntered)
            return;

        base.OnEnteredGate();

        _crowdController.ActivateCrowd();

        _isEntered = true;
    }
}
