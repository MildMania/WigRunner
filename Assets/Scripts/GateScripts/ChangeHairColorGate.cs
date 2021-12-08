using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHairColorGate : GateBase
{
    [SerializeField] private Color _color;
    [SerializeField] private HairSide _hairSide;

    public override void OnEnteredGate()
    {
        if (_isEntered)
            return;
        
        base.OnEnteredGate();

        print("COLOR CHANGE");

        Character.Instance.CharacterVisualController.SetDirtiness(0);
        Character.Instance.CharacterVisualController.SetHairColor(_color, _hairSide);

        _isEntered = true;
    }
}
