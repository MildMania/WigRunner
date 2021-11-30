using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

public class RemoveAddedParticleFromCharacter : MMTask
{
    [SerializeField] private GameObject _collectibleObject;

    public override ETaskStatus Execute(MonoBehaviour caller)
    {

        Character.Instance.CharacterVisualController.RemoveParticle(_collectibleObject);

        return ETaskStatus.Completed;
    }


}
