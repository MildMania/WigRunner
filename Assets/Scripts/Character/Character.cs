using UnityEngine;

public class Character : Singleton<Character>
{
    [SerializeField] private CharacterFSM _characterFSM = null;
    [SerializeField] private CollectibleCollector _collectibleCollector = null;
    [SerializeField] private ObstacleHandler _obstacleHandler = null;
    public CharacterFSM CharacterFSM => _characterFSM;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _obstacleHandler.OnObstacleCollided += _collectibleCollector.PopCollectible;
    }

    private void UnregisterFromEvents()
    {
        _obstacleHandler.OnObstacleCollided -= _collectibleCollector.PopCollectible;
    }
}