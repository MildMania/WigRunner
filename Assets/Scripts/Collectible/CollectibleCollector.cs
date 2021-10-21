using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollector : MonoBehaviour
{
    [SerializeField] private BaseCollectCommand _collectCommand;
    [SerializeField] private Transform _collectibleContainer;

    private BaseCollectibleDetector[] _collectibleDetectors;
    private List<Collectible> _collectedCollectibles;
    private BaseCollectCommand _collectCommandClone;
    public Action<Collectible> OnCollectibleCollected { get; set; }

    public BaseCollectibleDetector[] CollectibleDetectors
    {
        get
        {
            if (_collectibleDetectors == null)
            {
                _collectibleDetectors = GetComponentsInChildren<BaseCollectibleDetector>();
            }

            return _collectibleDetectors;
        }
    }


    private void Awake()
    {
        _collectedCollectibles = Character.Instance.CollectedCollectibles;

        foreach (var collectibleDetector in CollectibleDetectors)
        {
            collectibleDetector.OnDetected += OnDetected;
        }
    }

    private void OnDestroy()
    {
        foreach (var collectibleDetector in CollectibleDetectors)
        {
            collectibleDetector.OnDetected -= OnDetected;
        }

        foreach (var collectedCollectible in _collectedCollectibles)
        {
            collectedCollectible.StopCommandExecution();
        }
    }

    private void OnDetected(Collectible collectible)
    {
        if (_collectCommand != null)
        {
            CreateCommand();
        }

        collectible.OnCollected += OnCollected;
        collectible.TryCollect(_collectCommandClone);
    }

    private void OnCollected(Collectible collectible)
    {
        collectible.OnCollected -= OnCollected;
        _collectedCollectibles.Add(collectible);
        OnCollectibleCollected?.Invoke(collectible);
    }

    private void CreateCommand()
    {
        _collectCommandClone = Instantiate(_collectCommand);
        _collectCommandClone.TargetTransform = transform;
        _collectCommandClone.ParentTransform = _collectibleContainer;
        _collectCommandClone.CollectedCollectibles = _collectedCollectibles;
    }
}