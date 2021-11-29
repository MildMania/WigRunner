using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NoActionUncollectCommand", menuName = "ScriptableObjects/NoActionUncollectCommand",
    order = 1)]
public class NoActionUncollectCommand : BaseUncollectCommand
{
    protected override void ExecuteCustomActions(Collectible collectible, Action onUncollectCommandExecuted)
    {

    }

}
