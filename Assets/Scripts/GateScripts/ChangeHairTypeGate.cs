using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HairIcon
{
    public HairType HairType;
    public Material HairIconMaterial;
}

public class ChangeHairTypeGate : GateBase
{
    [SerializeField] private List<HairIcon> _hairIcons;
    
    [SerializeField] private HairType _hairType;
    [SerializeField] private Renderer _renderer;


    
    private Material GetHairIconMaterial(HairType hairType)
    {
        Material mat = _hairIcons[0].HairIconMaterial;

        foreach (var item in _hairIcons)
        {
            if (item.HairType == _hairType)
            {
                mat = item.HairIconMaterial;
                break;
            }
        }

        return mat;
    }

    public override void OnEnteredGate()
    {
        if (_isEntered)
            return;

        base.OnEnteredGate();

        Character.Instance.CharacterVisualController.ResetDirtiness();
        Character.Instance.CharacterVisualController.SetHairModelActive(_hairType);

        _isEntered = true;
    }

    private void OnValidate()
    {
        var mats = _renderer.materials;

        mats[2] = GetHairIconMaterial(_hairType);

        _renderer.materials = mats;
    }
}
