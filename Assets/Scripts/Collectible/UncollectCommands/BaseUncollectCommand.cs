using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUncollectCommand : ScriptableObject
{
    public List<Collectible> CollectedCollectibles { get; set; }
    public Action OnUncollectCommandStarted { get; }
    public Action OnUncollectCommandFinished { get; set; }

    public void Execute(Collectible collectible)
    {
        OnUncollectCommandStarted?.Invoke();

        ExecuteCustomActions(collectible,
                             () => OnUncollectCommandFinished?.Invoke());
    }

    public virtual void StopExecution()
    {
        CollectedCollectibles.ForEach(c =>
                        CoroutineRunner.Instance.StopCoroutine(c.MoveRoutine));
    }

    protected abstract void ExecuteCustomActions(
        Collectible collectible, Action onCollectCommandExecuted);
}