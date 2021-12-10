using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.Utilities;
using MMFramework.TasksV2;

using DG.Tweening;

public enum HairType
{
    Straight,
    Straight_2,
    Straight_3,
    Curly,
    Wavy,
    Dreadlock,
    PonyTail,
    PonyTail_2
}


[System.Serializable]
public class Hair
{
    public GameObject HairObject;
    public GameObject HairModel;
    public HairType HairType;
    public Transform AttachPointsRoot;

    public List<Cosmetic> Cosmetics;
}

public enum HairSide
{
    Left,
    Right
}


public class CharacterVisualController : MonoBehaviour
{
    [SerializeField] private List<Hair> _hairList;
    [SerializeField] private HairType _initialHairType = HairType.Straight;

    [SerializeField] private Material _hairMaterial;
    [SerializeField] private Color _initialHairColor;

    [SerializeField] private CollectibleController _collectibleController;

    [SerializeField] private MMTaskExecutor _onCleanedTasks;
    [SerializeField] private MMTaskExecutor _onAppearenceChangedTasks;


    [SerializeField] private BaseUncollectCommand _uncollectCommand;

    public GameObject AddedParticlesParent;

    private Dictionary<HairType, Hair> _hairByHairType = new Dictionary<HairType, Hair>();

    private HairType _currentHairType;
    public HairType CurrentHairType => _currentHairType;

    private Dictionary<GameObject, GameObject> _particleCarriersByGameObject = new Dictionary<GameObject, GameObject>();

    private float _currentDirtiness = 0;
    public float CurrentDirtiness => _currentDirtiness;

    private List<Transform> _currentAttachPoints = new List<Transform>();

    private CosmeticType _currentCosmetic = CosmeticType.None;

    private BaseUncollectCommand _uncollectCommandClone;


    private void Awake()
    {
        foreach (var item in _hairList)
        {
            _hairByHairType.Add(item.HairType, item);
        }

    }

    private void Start()
    {
        SetHairModelActive(_initialHairType, false);
        _currentHairType = _initialHairType;

        _hairMaterial.SetColor("_LeftColor2", _initialHairColor);
        _hairMaterial.SetColor("_LeftColor1", _initialHairColor);
        _hairMaterial.SetFloat("_LeftMaskAlpha", 0);

        _hairMaterial.SetColor("_RightColor2", _initialHairColor);
        _hairMaterial.SetColor("_RightColor1", _initialHairColor);
        _hairMaterial.SetFloat("_RightMaskAlpha", 0);

        _hairMaterial.SetFloat("_DirtMaskAlpha", _currentDirtiness);
    }


    public void SetHairModelActive(HairType hairType, bool executeOnAppearenceChangedTasks = true)
    {
        Hair hair;

        if(_hairByHairType.TryGetValue(hairType, out hair))
        {
            foreach (var item in _hairByHairType)
            {
                item.Value.HairObject.SetActive(false);
            }

            hair.HairObject.SetActive(true);

            _currentHairType = hairType;
        }

        if(_currentAttachPoints.Count != 0)
            _currentAttachPoints.Clear();
        _currentAttachPoints = new List<Transform>(hair.AttachPointsRoot.GetComponentsInChildren<Transform>());

        // add previous collectibles

        foreach (var collectible in _collectibleController.CollectedCollectibles)
        {
            if(collectible.CanAttach)
                collectible.transform.parent = GetAttachPoint();
        }

        if (_currentCosmetic != CosmeticType.None)
            EnableCosmetic(_currentCosmetic);

        if(executeOnAppearenceChangedTasks)
            _onAppearenceChangedTasks.Execute(this);
    }

    private void Clean()
    {
        if (_uncollectCommandClone == null)
        {
            _uncollectCommandClone = Instantiate(_uncollectCommand);
        }

        List<Collectible> collectibles = new List<Collectible>(Character.Instance.CollectibleController.CollectedCollectibles);

        foreach (var collectible in collectibles)
        {
            if (collectible.IsDirtyCollectible)
            {
                collectible.TryUncollect();
                _uncollectCommandClone.Execute(collectible);
            }
        }

    }



    public void SetDirtiness(float value)
    {
        if (value == 0)
        {
            _onCleanedTasks.Execute(this);
            Clean();
        }

         _currentDirtiness = value;

        _hairMaterial.SetFloat("_DirtMaskAlpha", _currentDirtiness);
    }

    public void ResetDirtiness()
    {
        //Clean();

        _currentDirtiness = 0;
        _hairMaterial.SetFloat("_DirtMaskAlpha", _currentDirtiness);
    }

    public void SetGlitter()
    {

    }

    public IEnumerator StartColorRoutine(Material material, string propertyName, Action onColorRoutineEnded)
    {
        float val = 0;

        while(val <= 1)
        {
            material.SetFloat(propertyName, val);

            val += 0.1f;
            yield return null;
        }

        onColorRoutineEnded();
    }

    public void SetHairColor(Color newColor, HairSide side)
    {
        var hair = _hairByHairType[_currentHairType];

        // there is some kind of strange situation about directions in material

        if (side.Equals(HairSide.Right))
        {
            // first set color

            _hairMaterial.SetColor("_LeftColor2", newColor);

            StartCoroutine(StartColorRoutine(_hairMaterial, "_LeftMaskAlpha", () => {
                //-- when tween is finished

                _hairMaterial.SetColor("_LeftColor1", newColor);
                // set mask alpha 0

                _hairMaterial.SetFloat("_LeftMaskAlpha", 0);

            }));        

        }
        else
        {
            _hairMaterial.SetColor("_RightColor2", newColor);

            StartCoroutine(StartColorRoutine(_hairMaterial, "_RightMaskAlpha", () => {
                //-- when tween is finished

                _hairMaterial.SetColor("_RightColor1", newColor);
                // set mask alpha 0

                _hairMaterial.SetFloat("_RightMaskAlpha", 0);

            }));
        }
    }

    public Transform GetAttachPoint()
    {
        var point = _currentAttachPoints[_currentAttachPoints.Count - 1];

        _currentAttachPoints.RemoveAt(_currentAttachPoints.Count - 1);
        return point;
    }

    public void AddParticle(GameObject adder, GameObject particleCarrier)
    {
        _particleCarriersByGameObject.Add(adder, particleCarrier);

        particleCarrier.transform.parent = AddedParticlesParent.transform;
        particleCarrier.transform.localPosition = Vector3.zero;
        particleCarrier.SetActive(true);
    }

    public void RemoveParticle(GameObject adder)
    {
        var particleCarrier = _particleCarriersByGameObject[adder];

        particleCarrier.transform.parent = null;
        particleCarrier.SetActive(false);

        _particleCarriersByGameObject.Remove(adder);
    }

    public void EnableCosmetic(CosmeticType cosmeticType, bool executeAppearenceChangeTasks = true)
    {
        var hair = _hairByHairType[_currentHairType];

        foreach (var cosmetic in hair.Cosmetics)
        {
            if(cosmetic.CosmeticType == cosmeticType)
            {
                cosmetic.CosmeticObject.SetActive(true);
            }
            else
            {
                cosmetic.CosmeticObject.SetActive(false);
            }
        }

        _currentCosmetic = cosmeticType;

        if (executeAppearenceChangeTasks)
            _onAppearenceChangedTasks.Execute(this);
    }

    public Hair GetHairWithHairType(HairType hairType)
    {
        return _hairByHairType[hairType];
    }
    

}
