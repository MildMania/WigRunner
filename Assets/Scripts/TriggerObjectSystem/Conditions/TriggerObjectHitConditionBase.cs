using UnityEngine;

public abstract class TriggerObjectHitConditionBase : MonoBehaviour
{
    public abstract bool CanDetectHit(TriggerObject targetTriggerObject);
}
