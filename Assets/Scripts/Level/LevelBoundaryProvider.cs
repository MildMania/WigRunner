using UnityEngine;

public class LevelBoundaryProvider : Singleton<LevelBoundaryProvider>
{
    [SerializeField] private Transform _leftBoundary = null;

    [SerializeField] private Transform _rightBoundary = null;

    public Vector3 GetLeftBoundary()
    {
        return _leftBoundary.position;
    }

    public Vector3 GetRightBoundary()
    {
        return _rightBoundary.position;
    }
}