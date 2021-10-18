using UnityEngine;

public class TriggerBasedCollectibleDetector : BaseCollectibleDetector
{
    [SerializeField] private TriggerObjectHitController _collectibleHitController;


    private void Awake()
    {
        _collectibleHitController.OnHitTriggerObject += OnHitTriggerObject;
    }

    private void OnDestroy()
    {
        _collectibleHitController.OnHitTriggerObject -= OnHitTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject triggerObject)
    {
        Collectible collectible = triggerObject.GetComponentInParent<Collectible>();
        LastDetected = collectible;
        OnDetected?.Invoke(collectible);
    }
}