using System;
using UnityEngine;

public class CollisionData
{
    public Transform Other { get; }
    
    public bool IsCollisionEnter { get; }

    public CollisionData(Transform other, bool isCollisionEnter)
    {
        Other = other;

        IsCollisionEnter = isCollisionEnter;
    }
}

public class SimpleCollisionReceiver : MonoBehaviour, ICollisionReceiver
{
   
    public Action<CollisionData> OnTriggerEnterEvent { get; set; }
    public Action<CollisionData> OnTriggerExitEvent { get; set; }
    public Action<CollisionData> OnCollisionEnterEvent { get; set; }
    public Action<CollisionData> OnCollisionExitEvent { get; set; }
    
    public void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(new CollisionData(other.transform, true));
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(new CollisionData(collision.collider.transform, true));
    }

    public void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(new CollisionData(other.transform, false));
    }

    public void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent?.Invoke(new CollisionData(collision.collider.transform, false));
    }
}
