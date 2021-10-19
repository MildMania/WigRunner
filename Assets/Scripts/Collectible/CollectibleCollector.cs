﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectibleCollector : MonoBehaviour
{
    [SerializeField] private BaseCollectCommand _collectCommand;
    [SerializeField] private Transform _collectibleContainer;

    private BaseCollectibleDetector[] _collectibleDetectors;
    private List<Collectible> _collectedCollectibles = new List<Collectible>();
    BaseCollectCommand _collectCommandClone;
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

        if (_collectCommandClone != null)
        {
            _collectCommandClone.StopExecution();
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