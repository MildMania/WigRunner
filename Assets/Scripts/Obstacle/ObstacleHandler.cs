using System;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    private BaseObstacleDetector[] _obstacleDetectors;
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
    }

    private void OnDetected(Obstacle obstacle)
    {
        obstacle.OnCollided += OnCollided;
        obstacle.TryCollide();
    }

    private void OnCollided(Obstacle obstacle)
    {
        OnObstacleCollided?.Invoke(obstacle);
    }
}