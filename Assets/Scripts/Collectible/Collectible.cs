using MMFramework.TasksV2;
using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollectedTasks;

    public Collider Collider;
    public bool IsCollected { get; private set; }
    public Action<Collectible> OnCollected;


    public bool TryCollect(BaseCollectCommand collectCommand = default)
    {
        if (IsCollected)
        {
            return false;
        }

        IsCollected = true;
        _onCollectedTasks?.Execute(this);


        if (collectCommand != null)
        {
            collectCommand.OnCollectCommandFinished += () => OnCollected?.Invoke(this);
            collectCommand.Execute(this);
        }
        else
        {
            OnCollected?.Invoke(this);
        }

        return true;
    }
}