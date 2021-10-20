using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private BaseUncollectCommand _uncollectCommand;

    private BaseObstacleDetector[] _obstacleDetectors;
    private List<Collectible> _collectedCollectibles;
    private BaseUncollectCommand _uncollectCommandClone;

    public Action<Obstacle> OnObstacleCollided { get; set; }

    public BaseObstacleDetector[] ObstacleDetectors
    {
        get
        {
            if (_obstacleDetectors == null)
            {
                _obstacleDetectors = GetComponentsInChildren<BaseObstacleDetector>();
            }

            return _obstacleDetectors;
        }
    }

    private void Awake()
    {
        _collectedCollectibles = Character.Instance.CollectedCollectibles;

        foreach (var obstacleDetector in ObstacleDetectors)
        {
            obstacleDetector.OnDetected += OnDetected;
        }
    }

    private void OnDestroy()
    {
        foreach (var obstacleDetector in ObstacleDetectors)
        {
            obstacleDetector.OnDetected -= OnDetected;
        }

        if (_uncollectCommandClone != null)
        {
            _uncollectCommandClone.StopExecution();
        }
    }

    private void OnDetected(Obstacle obstacle)
    {
        if (_uncollectCommand != null)
        {
            CreateCommand();
        }

        // Temporary
        RemoveLastCollectible();

        obstacle.OnCollided += OnCollided;
        obstacle.TryCollide();
    }

    private void OnCollided(Obstacle obstacle)
    {
        OnObstacleCollided?.Invoke(obstacle);
    }

    private void CreateCommand()
    {
        _uncollectCommandClone = Instantiate(_uncollectCommand);
        _uncollectCommandClone.CollectedCollectibles = _collectedCollectibles;
    }

    private void RemoveLastCollectible()
    {
        if (_collectedCollectibles.Count > 0)
        {
            var removedCollectible = _collectedCollectibles
                                            [_collectedCollectibles.Count - 1];

            removedCollectible.TryUncollect(_uncollectCommand);
            _collectedCollectibles.Remove(removedCollectible);
        }
    }
}