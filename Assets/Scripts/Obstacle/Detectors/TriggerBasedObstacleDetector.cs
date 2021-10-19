using UnityEngine;

public class TriggerBasedObstacleDetector : BaseObstacleDetector
{
    [SerializeField] private TriggerObjectHitController _obstacleHitController;

    private void Awake()
    {
        _obstacleHitController.OnHitTriggerObject += OnHitTriggerObject;

    }

    private void OnDestroy()
    {
        _obstacleHitController.OnHitTriggerObject -= OnHitTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject triggerObject)
    {
        var obstacle = triggerObject.GetComponentInParent<Obstacle>();
        LastDetected = obstacle;
        OnDetected?.Invoke(obstacle);
    }
}