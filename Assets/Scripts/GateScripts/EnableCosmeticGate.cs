using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CosmeticType
{
    Crown,
    Ribbon,
    None
}

[System.Serializable]
public class Cosmetic
{
    public CosmeticType CosmeticType;
    public GameObject CosmeticObject;
}

public class EnableCosmeticGate : GateBase
{
    [SerializeField] CosmeticType _cosmeticType;

    public override void OnEnteredGate()
    {
        if (_isEntered)
            return;

        base.OnEnteredGate();

        Character.Instance.CharacterVisualController.ResetDirtiness();
        Character.Instance.CharacterVisualController.EnableCosmetic(_cosmeticType);

        _isEntered = true;
    }
}
