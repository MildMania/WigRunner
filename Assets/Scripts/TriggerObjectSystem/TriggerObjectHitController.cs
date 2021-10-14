using System;
using System.Linq;
using MMFramework.TasksV2;
using UnityEngine;

public class TriggerObjectHitController : MonoBehaviour
{
    [SerializeField] private SimpleCollisionReceiver _collisionReceiver = null;
    
    [SerializeField] private LayerMask _targetTriggerObjectMask = default;

    [SerializeField] private MMTaskExecutor _hitTriggerObjectTaskExecutor = null;

    [SerializeField] private MMTaskExecutor _hitEndedTriggerObjectTaskExecutor = null;

    [SerializeField] private bool _triggerOnce = false;

    private bool _isTriggeredBefore = false;
    
    public CollisionData LastCollisionData { get; private set; }
    
    public TriggerObject LastHitTriggerObject { get; private set; }
    
    private TriggerObjectHitConditionBase[] _triggerObjectHitConditions;
    private TriggerObjectHitConditionBase[] _TriggerObjectHitConditions
    {
        get
        {
            if (_triggerObjectHitConditions == null)
            {
                _triggerObjectHitConditions = GetComponentsInChildren<TriggerObjectHitConditionBase>();
            }

            return _triggerObjectHitConditions;
        }
    }

    #region Events

    public Action<TriggerObject> OnHitTriggerObject { get; set; }
    
    public Action<TriggerObject> OnHitEndedTriggerObject { get; set; }

    #endregion

    private void Awake()
    {
        RegisterToHitDetector();
    }

    private void OnDestroy()
    {
        UnregisterFromHitDetector();
    }

    private void RegisterToHitDetector()
    {
        _collisionReceiver.OnTriggerEnterEvent += OnHit;
        _collisionReceiver.OnCollisionEnterEvent += OnHit;

        _collisionReceiver.OnTriggerExitEvent += OnHitEnded;
        _collisionReceiver.OnCollisionExitEvent += OnHitEnded;
    }

    private void UnregisterFromHitDetector()
    {
        _collisionReceiver.OnTriggerEnterEvent -= OnHit;
        _collisionReceiver.OnCollisionEnterEvent -= OnHit;
        
        _collisionReceiver.OnTriggerExitEvent -= OnHitEnded;
        _collisionReceiver.OnCollisionExitEvent -= OnHitEnded;
    }

    private void OnHit(CollisionData collisionData)
    {
        if (LayerMaskExtensions.IsInLayerMask(_targetTriggerObjectMask, collisionData.Other.gameObject.layer))
        {
            LastCollisionData = collisionData;
            
            TriggerObject triggerObject = collisionData.Other.GetComponentInParent<TriggerObject>();

            if (triggerObject == null)
            {
                return;
            }
            
            if (!CanDetectHit(triggerObject))
            {
                return;
            }

            if (_triggerOnce && _isTriggeredBefore)
            {
                return;
            }

            _isTriggeredBefore = true;
            
            LastHitTriggerObject = triggerObject;

            _hitTriggerObjectTaskExecutor.Execute(this);

            OnHitTriggerObject?.Invoke(triggerObject);
        }
    }
    
    private void OnHitEnded(CollisionData collisionData)
    {
        if (LayerMaskExtensions.IsInLayerMask(_targetTriggerObjectMask, collisionData.Other.gameObject.layer))
        {
            TriggerObject triggerObject = collisionData.Other.GetComponentInParent<TriggerObject>();
            
            if (triggerObject == null)
            {
                return;
            }
            
            if (!CanDetectHit(triggerObject))
            {
                return;
            }
        
            _hitEndedTriggerObjectTaskExecutor.Execute(this);
            
            OnHitEndedTriggerObject?.Invoke(triggerObject);
        }
    }

    private bool CanDetectHit(TriggerObject triggerObject)
    {
        return _TriggerObjectHitConditions.All(val => val.CanDetectHit(triggerObject));
    }
}
