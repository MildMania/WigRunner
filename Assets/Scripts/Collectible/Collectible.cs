using MMFramework.TasksV2;
using System;
using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollectedTasks;
    [SerializeField] private MMTaskExecutor _onUncollectedTasks;

    public Collider Collider;
    public bool IsCollected { get; private set; }
    public bool IsUncollected { get; private set; }

    public Action<Collectible> OnCollected;
    public Action<Collectible> OnUncollected;

    public IEnumerator MoveRoutine;

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

    public bool TryUncollect(BaseUncollectCommand uncollectCommand = default)
    {
        if (IsUncollected)
        {
            return false;
        }

        IsUncollected = true;
        _onUncollectedTasks?.Execute(this);

        if (uncollectCommand != null)
        {
            uncollectCommand.OnUncollectCommandFinished += () => OnUncollected?.Invoke(this);
            uncollectCommand.Execute(this);
        }
        else
        {
            OnUncollected?.Invoke(this);
        }

        return true;
    }
}