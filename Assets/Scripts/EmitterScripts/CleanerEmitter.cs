using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerEmitter : EmitterBase
{
    [SerializeField] private BaseUncollectCommand _uncollectCommand;

    private BaseUncollectCommand _uncollectCommandClone;

    protected override void OnCharacterEntered()
    {
        base.OnCharacterEntered();

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
}
