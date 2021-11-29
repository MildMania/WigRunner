using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public class AddRandomSparkleToCharacterTask : MMTask
{
    [SerializeField] private List<GameObject> _sparkleParticleCarriers;

    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        if (_sparkleParticleCarriers.Count == 0)
            return ETaskStatus.Completed;

        var indx = Random.Range(0, _sparkleParticleCarriers.Count - 1);

        var obj = _sparkleParticleCarriers[indx];

        obj.transform.parent = Character.Instance.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(true);

        return ETaskStatus.Completed;
    }

}
