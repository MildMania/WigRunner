using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public enum ECrowdJoy
{
    Low,
    Middle,
    High
}


public class CrowdController : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCrowdActivatedTasks;

    [SerializeField] private List<NpcController> _npcControllers;


    private ECrowdJoy GetCrowdJoy()
    {
        return ECrowdJoy.Low;
    }

    public void ActivateCrowd()
    {
        if(_onCrowdActivatedTasks != null)
        {
            _onCrowdActivatedTasks.Execute(this);
        }

        var crowdJoy = GetCrowdJoy();

        foreach (var npc in _npcControllers)
        {
            npc.Activate(crowdJoy);
        }


    }
}
