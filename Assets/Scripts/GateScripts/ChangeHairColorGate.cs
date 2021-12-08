using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHairColorGate : GateBase
{
    [SerializeField] private Color _color;
    [SerializeField] private HairSide _hairSide;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private ParticleSystem _ps;

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


    private void OnValidate()
    {
        var col = _ps.colorOverLifetime;
        col.color = _color;

        var mats = _renderer.materials;

        mats[0].color = _color;
        mats[0].SetColor("_EmissionColor", _color);


        _renderer.materials = mats;
    }
}
