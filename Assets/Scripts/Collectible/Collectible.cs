using MMFramework.TasksV2;
using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollectedTasks;

    public bool IsCollected { get; private set; }
    public Action<Collectible> OnCollected;


    public bool TryCollect()
    {
        if (IsCollected)
        {
            return false;
        }

        gameObject.SetActive(false);
        IsCollected = true;
        _onCollectedTasks?.Execute(this);
        OnCollected?.Invoke(this);
        return true;
    }
}