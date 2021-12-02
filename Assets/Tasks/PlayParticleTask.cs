using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;
public class PlayParticleTask : MMTask
{
    [SerializeField] private ParticleEffectPlayer _particleEffectPlayer;
    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        _particleEffectPlayer.Play();

        return ETaskStatus.Completed;
    }


}
