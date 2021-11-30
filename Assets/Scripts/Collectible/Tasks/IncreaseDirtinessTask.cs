using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public class IncreaseDirtinessTask : MMTask
{
    [SerializeField] private float _amount = 0.2f;

    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        var controller = Character.Instance.CharacterVisualController;
        controller.SetDirtiness(controller.CurrentDirtiness + _amount);
        return ETaskStatus.Completed;
    }


}
