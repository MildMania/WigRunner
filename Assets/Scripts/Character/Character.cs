using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    [SerializeField] private CharacterFSM _characterFSM = null;

    private List<Collectible> _collectedCollectibles = new List<Collectible>();

    public CharacterFSM CharacterFSM => _characterFSM;

    public List<Collectible> CollectedCollectibles { get => _collectedCollectibles; set => _collectedCollectibles = value; }
}