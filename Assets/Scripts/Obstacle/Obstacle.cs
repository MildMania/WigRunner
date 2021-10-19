using MMFramework.TasksV2;
using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollidedTasks;

    public Collider Collider;
    public bool IsCollided { get; private set; }
    public Action<Obstacle> OnCollided;

    public bool TryCollide()
    {
        if (IsCollided)
        {
            return false;
        }

        IsCollided = true;
        _onCollidedTasks?.Execute(this);

        OnCollided?.Invoke(this);

        return true;
    }
}