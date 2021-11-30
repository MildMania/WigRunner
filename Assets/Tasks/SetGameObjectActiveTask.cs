using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public class SetGameObjectActiveTask : MMTask
{
    [SerializeField] private GameObject _object;
    [SerializeField] private bool _value;

    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        if(_object == null)
            return ETaskStatus.Completed;

        _object.SetActive(_value);

        return ETaskStatus.Completed;
    }

}
