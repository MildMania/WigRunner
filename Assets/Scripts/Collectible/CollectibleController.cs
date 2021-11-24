using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private List<Collectible> _collectedCollectibles = new List<Collectible>();

    public List<Collectible> CollectedCollectibles
    {
        get => _collectedCollectibles;
        set => _collectedCollectibles = value;
    }
}