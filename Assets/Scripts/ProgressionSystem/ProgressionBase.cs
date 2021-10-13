using UnityEngine;

namespace ProgressionSystem
{
    public abstract class ProgressionBase : MonoBehaviour
    {
        public abstract EProgressionResult GetProgressionType();
        public abstract bool CheckProgression();
    }
}