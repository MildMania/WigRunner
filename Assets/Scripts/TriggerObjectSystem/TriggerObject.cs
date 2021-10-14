using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private ETriggerObject _triggerObjectType = ETriggerObject.None;
    public ETriggerObject TriggerObjectType => _triggerObjectType;
}
