using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTrack : TrackBase
{
    protected override void OnStayCustomActions()
    {
        base.OnStayCustomActions();

        var curDirt = Character.Instance.CharacterVisualController.CurrentDirtiness;
        Character.Instance.CharacterVisualController.SetDirtiness(curDirt + 0.001f, false);
    }
}
