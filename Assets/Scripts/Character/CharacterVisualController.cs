using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HairType
{
    Straight,
    Curly,
    Wavy,
    Dreadlock
}


[System.Serializable]
public class Hair
{
    public GameObject HairObject;
    public HairType HairType;

}


public class CharacterVisualController : MonoBehaviour
{
    [SerializeField] private List<Hair> _hairList;
    [SerializeField] private HairType _initialHairType = HairType.Straight;

    private Dictionary<HairType, GameObject> _hairByHairType = new Dictionary<HairType, GameObject>();


    private void Awake()
    {
        foreach (var item in _hairList)
        {
            _hairByHairType.Add(item.HairType, item.HairObject);
        }
    }

    private void Start()
    {
        SetHairModelActive(_initialHairType);
    }


    public void SetHairModelActive(HairType hair)
    {
        GameObject hairObj;

        if(_hairByHairType.TryGetValue(hair, out hairObj))
        {
            foreach (var item in _hairByHairType)
            {
                item.Value.SetActive(false);
            }

            hairObj.SetActive(true);
        }
    }

    public void SetDirtiness()
    {

    }

    public void SetGlitter()
    {

    }

    public void SetHairColor()
    {

    }
    

}
