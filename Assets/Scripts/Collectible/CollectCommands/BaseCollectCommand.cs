using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectCommand : ScriptableObject
{
    public Action OnCollectCommandStarted { get; }
    public Action OnCollectCommandFinished { get; set; }

    public Transform ParentTransform { protected get; set; }
    public Vector3 TargetPosition { get; protected set; }
    public Transform CollectibleContainerTransform { protected get; set; }

    public List<Collectible> CollectedCollectibles { get; set; }

    public void Execute(Collectible collectible)
    {
        CalculateNextCollectiblePosition(collectible);
        OnCollectCommandStarted?.Invoke();
        ExecuteCustomActions(collectible, onCollectCommandExecuted);

        void onCollectCommandExecuted()
        {
            OnCollectCommandFinished?.Invoke();
        }
    }

    public virtual void StopExecution()
    {
    }


    protected abstract void ExecuteCustomActions(
        Collectible collectible, Action onCollectCommandExecuted);

    protected abstract void
        CalculateNextCollectiblePosition(Collectible collectible);
}