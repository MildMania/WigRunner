using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttachCollectCommand", menuName = "ScriptableObjects/AttachCollectCommand",
    order = 1)]
public class AttachCollectCommand : BaseCollectCommand
{
    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {

    }




    protected override void ExecuteCustomActions(Collectible collectible, Action onCollectCommandExecuted)
    {
        if (!collectible.CanAttach)
            return;

        var point = Character.Instance.CharacterVisualController.GetAttachPoint();
        collectible.transform.position = point.position;

        collectible.transform.parent = point;

        OnCollectCommandFinished?.Invoke();
    }
}
