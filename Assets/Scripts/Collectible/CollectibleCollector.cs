using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollector : MonoBehaviour
{
    [SerializeField] private BaseCollectCommand _collectCommand;
    [SerializeField] private Transform _collectibleContainer;
    [SerializeField] private CollectibleController _collectibleController;
    [SerializeField] private Transform[] _leadingTransforms;

    private List<Transform>[] _targetTransforms;

    private BaseCollectibleDetector[] _collectibleDetectors;
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
        if (_leadingTransforms != null)
        {
            _targetTransforms = new List<Transform>[_leadingTransforms.Length];
            for (int i = 0; i < _targetTransforms.Length; i++)
            {
                _targetTransforms[i] = new List<Transform> { _leadingTransforms[i] };
            }
        }


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

        foreach (var collectedCollectible in _collectibleController.CollectedCollectibles)
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
        collectible.OnUncollected += OnUncollected;
        _collectibleController.CollectedCollectibles.Add(collectible);
        OnCollectibleCollected?.Invoke(collectible);
    }

    private void OnUncollected(Collectible collectible)
    {
        collectible.OnUncollected -= OnUncollected;
        _collectibleController.CollectedCollectibles.Remove(collectible);
    }

    private void CreateCommand()
    {
        _collectCommandClone = Instantiate(_collectCommand);
        _collectCommandClone.TargetTransform = transform;
        _collectCommandClone.ParentTransform = _collectibleContainer;
        _collectCommandClone.CollectedCollectibles = _collectibleController.CollectedCollectibles;
        _collectCommandClone.TargetTransforms = _targetTransforms;
    }
}