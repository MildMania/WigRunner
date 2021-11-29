using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoActionCollectCommand", menuName = "ScriptableObjects/NoActionCollectCommand",
    order = 1)]
public class NoActionCollectCommand : BaseCollectCommand
{
    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {
        
    }

    protected override void ExecuteCustomActions(Collectible collectible, Action onCollectCommandExecuted)
    {
        onCollectCommandExecuted?.Invoke();
    }

}
