using MVVM;
using UnityEngine;

public class EnableViewPLD : IPLDBase
{
    public bool IsEnable { get; }

    public EnableViewPLD(bool isEnable)
    {
        IsEnable = isEnable;
    }
}

public class EnableView : NonInteractableViewBase<EnableViewPLD>
{
    [SerializeField] private Transform _targetTransform = null;

    protected override void ParsePLD(EnableViewPLD pld)
    {
        _targetTransform.gameObject.SetActive(pld.IsEnable);
    }
}
