using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private BaseUncollectCommand _uncollectCommand;
    [SerializeField] private CollectibleController _collectibleController;

    private BaseObstacleDetector[] _obstacleDetectors;
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

        Collectible collectible = GetLastCollectible();
        if (collectible != null)
        {
            collectible.OnUncollected += OnUncollected;
            collectible.TryUncollect(_uncollectCommand);
        }

        obstacle.OnCollided += OnCollided;
        obstacle.TryCollide();
    }

    private void OnUncollected(Collectible collectible)
    {
        collectible.OnUncollected -= OnUncollected;
        _collectibleController.CollectedCollectibles.Remove(collectible);
    }


    private void OnCollided(Obstacle obstacle)
    {
        OnObstacleCollided?.Invoke(obstacle);
    }

    private void CreateCommand()
    {
        _uncollectCommandClone = Instantiate(_uncollectCommand);
    }

    private Collectible GetLastCollectible()
    {
        Collectible collectible = null;
        if (_collectibleController.CollectedCollectibles.Count > 0)
        {
            collectible = _collectibleController.CollectedCollectibles[_collectibleController.CollectedCollectibles.Count - 1];
        }

        return collectible;
    }
}