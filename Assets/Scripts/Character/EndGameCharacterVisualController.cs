using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HairPivot
{
    public HairType HairType;
    public Transform Pivot;
}


public class EndGameCharacterVisualController : MonoBehaviour
{
    [SerializeField] private List<HairPivot> _hairPivots;

    private Dictionary<HairType, HairPivot> _hairPivotsByHairType = new Dictionary<HairType, HairPivot>();

    private void Awake()
    {
        foreach (var item in _hairPivots)
        {
            _hairPivotsByHairType.Add(item.HairType, item);
        }
    }

    public HairPivot GetHairPivotWithHairType(HairType hairType)
    {
        return _hairPivotsByHairType[hairType];
    }
}
