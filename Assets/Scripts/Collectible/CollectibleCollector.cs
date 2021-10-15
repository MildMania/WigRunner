using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectibleCollector : MonoBehaviour
{
    private BaseCollectibleDetector[] _collectibleDetectors;

    private List<Collectible> _collectedCollectibles = new List<Collectible>();

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
            collectibleDetector.OnCollectibleDetected += OnCollectibleDetected;
        }
    }


    private void OnDestroy()
    {
        foreach (var collectibleDetector in CollectibleDetectors)
        {
            collectibleDetector.OnCollectibleDetected -= OnCollectibleDetected;
        }
    }

    private void OnCollectibleDetected(Collectible collectible)
    {
        if (!collectible.TryCollect())
        {
            return;
        }

        _collectedCollectibles.Add(collectible);
        OnCollectibleCollected?.Invoke(collectible);
    }
}