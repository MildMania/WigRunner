using System.Collections.Generic;
using UnityEngine;

namespace ColliderScripts
{
    public class ColliderHolder : MonoBehaviour
    {
        [SerializeField] private List<Collider> _colliders = null;

        public void SetColliderEnable(bool value)
        {
            _colliders.ForEach(i => i.enabled = value);
        }
    }
}