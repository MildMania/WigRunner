using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHairColorGate : GateBase
{
    [SerializeField] private Color _color;

    public override void OnEnteredGate()
    {
        base.OnEnteredGate();

        Character.Instance.CharacterVisualController.SetHairColor(_color);
    }
}
