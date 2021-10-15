using System;
using UnityEngine;


[RequireComponent(typeof(CollectibleFovController))]
public class FovBasedCollectibleDetector : BaseCollectibleDetector
{
    [SerializeField] private CollectibleFovController _collectibleFOVController;

    private void Awake()
    {
        SubscribeToFovController();
        _collectibleFOVController.SetActive(true);
    }

    private void OnDestroy()
    {
        _collectibleFOVController.SetActive(false);
        UnsubscribeFromFovController();
    }

    private void SubscribeToFovController()
    {
        _collectibleFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _collectibleFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }

    private void UnsubscribeFromFovController()
    {
        _collectibleFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _collectibleFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }

    private void OnTargetEnteredFieldOfView(Collectible collectible)
    {
        OnCollectibleDetected?.Invoke(collectible);
    }

    private void OnTargetExitedFieldOfView(Collectible collectible)
    {
    }
}