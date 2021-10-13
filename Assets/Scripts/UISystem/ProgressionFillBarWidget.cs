using UnityEngine;
using UnityWeld.Binding;
using MMUtilities = MMFramework.Utilities.Utilities;

[Binding]
public class ProgressionFillBarWidget : FillBarWidget
{
    [SerializeField] private Transform _characterTransform = null;

    private float _characterStartPosition;

    public override float FillBarSize => 1;

    private void Start()
    {
        // TODO:
        //_characterStartPosition = _characterTransform.position.x;
    }

    private void Update()
    {
        if (State != EState.Active)
            return;

        UpdateBar();
    }

    private void UpdateBar()
    {
        // TODO:
        //TargetNormalizedValue = MMUtilities.Normalize(
        //    _characterTransform.position.x,
        //    Finishline.Instance.transform.position.x,
        //    _characterStartPosition,
        //    1,
        //    0);
    }
}