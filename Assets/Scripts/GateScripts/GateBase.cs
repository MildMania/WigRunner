using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBase : MonoBehaviour
{
    protected bool _isEntered = false;

    [SerializeField] protected BaseUncollectCommand _uncollectCommand;

    private BaseUncollectCommand _uncollectCommandClone;

    public virtual void OnEnteredGate()
    {
        if (_uncollectCommandClone == null)
        {
            _uncollectCommandClone = Instantiate(_uncollectCommand);
        }

        List<Collectible> collectibles = new List<Collectible>(Character.Instance.CollectibleController.CollectedCollectibles);

        foreach (var collectible in collectibles)
        {
            if (collectible.IsDirtyCollectible)
            {
                collectible.TryUncollect();
                _uncollectCommandClone.Execute(collectible);
            }
        }

        Character.Instance.CharacterVisualController.SetDirtiness(0);

    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnteredGate();
    }
}
