using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public class CrowdGate : GateBase
{
    [SerializeField] private CrowdController _crowdController;
    [SerializeField] private MMTaskExecutor _onCrowdActivatedTasks;

    public override void OnEnteredGate()
    {
        if (_isEntered)
            return;

        base.OnEnteredGate();

        if (_onCrowdActivatedTasks != null)
            _onCrowdActivatedTasks.Execute(this);

        _crowdController.ActivateCrowd();

        _isEntered = true;
    }
}
