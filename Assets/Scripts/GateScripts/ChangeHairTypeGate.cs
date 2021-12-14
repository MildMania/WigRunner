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


    [SerializeField] private Material _matGate;
    [SerializeField] private Material _matPlain;

    [SerializeField] private Color _baseColor;

    //[ColorUsage(true, true)]
    //[SerializeField] private Color _emissionColor;

    
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
        Character.Instance.CharacterVisualController.SetBaseHairColor(_baseColor);

        _isEntered = true;
    }

    private void OnValidate()
    {
        var mats = _renderer.materials;

        mats[0] = _matGate;
        mats[2] = _matPlain;

        mats[1] = GetHairIconMaterial(_hairType);
        mats[1].SetColor("_BaseColor", _baseColor);
        
        //mats[1].SetColor("_EmissionColor", _emissionColor);
        //mats[1].DisableKeyword("_EMISSION");

        _renderer.materials = mats;
    }
}
