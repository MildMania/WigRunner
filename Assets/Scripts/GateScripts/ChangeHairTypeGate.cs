using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHairTypeGate : GateBase
{
    [SerializeField] private HairType _hairType;
    public override void OnEnteredGate()
    {
        if (_isEntered)
            return;

        base.OnEnteredGate();

        Character.Instance.CharacterVisualController.SetDirtiness(0);
        Character.Instance.CharacterVisualController.SetHairModelActive(_hairType);

        _isEntered = true;
    }
}
