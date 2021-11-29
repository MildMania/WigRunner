using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHairTypeGate : GateBase
{
    [SerializeField] private HairType _hairType;
    public override void OnEnteredGate()
    {
        base.OnEnteredGate();

        Character.Instance.CharacterVisualController.SetHairModelActive(_hairType);
    }
}
