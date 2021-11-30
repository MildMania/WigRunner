using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public class AddRandomParticleToCharacterTask : MMTask
{
    [SerializeField] private GameObject _collectibleObject;
    [SerializeField] private List<GameObject> _particleCarriers;

    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        if (_particleCarriers.Count == 0)
            return ETaskStatus.Completed;

        var indx = Random.Range(0, _particleCarriers.Count - 1);

        var particleCarrier = _particleCarriers[indx];

        Character.Instance.CharacterVisualController.AddParticle(_collectibleObject, particleCarrier);

        return ETaskStatus.Completed;
    }

}
