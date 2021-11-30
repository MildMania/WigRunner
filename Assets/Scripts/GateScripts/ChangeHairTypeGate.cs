using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHairTypeGate : GateBase
{
    [SerializeField] private HairType _hairType;
    public override void OnEnteredGate()
    {
        base.OnEnteredGate();

        if (_isEntered)
            return;

        Character.Instance.CharacterVisualController.SetHairModelActive(_hairType);

        _isEntered = true;
    }
}
