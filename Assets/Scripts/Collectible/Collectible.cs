﻿using MMFramework.TasksV2;
using System;
using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollectedTasks;
    [SerializeField] private MMTaskExecutor _onUncollectedTasks;

    [SerializeField] private bool _isDirtyCollectible = false;
    public bool IsDirtyCollectible => _isDirtyCollectible;

    [SerializeField] private bool _canAttach = false;
    public bool CanAttach => _canAttach;


    public Collider Collider;
    public bool IsCollected { get; private set; }
    public bool IsUncollected { get; private set; }

    public Action<Collectible> OnCollected;
    public Action<Collectible> OnUncollected;

    public IEnumerator MoveRoutine;

    private BaseCollectCommand _collectCommand;
    private BaseUncollectCommand _uncollectCommand;


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
            _collectCommand = collectCommand;
            collectCommand.OnCollectCommandFinished += OnCollectCommandFinished;
            collectCommand.Execute(this);
        }
        else
        {
            OnCollected?.Invoke(this);
        }

        return true;
    }

    private void OnCollectCommandFinished()
    {
        OnCollected?.Invoke(this);
        _collectCommand.OnCollectCommandFinished -= OnCollectCommandFinished;
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
            _uncollectCommand = uncollectCommand;
            uncollectCommand.OnUncollectCommandFinished += OnUncollectCommandFinished;

            uncollectCommand.Execute(this);
        }
        else
        {
            OnUncollected?.Invoke(this);
        }

        return true;
    }

    private void OnUncollectCommandFinished()
    {
        OnUncollected?.Invoke(this);
        _uncollectCommand.OnUncollectCommandFinished -= OnUncollectCommandFinished;
    }


    public void StopCommandExecution()
    {
        _collectCommand.StopExecution();
    }
}