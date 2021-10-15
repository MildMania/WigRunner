using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectibleDetector : MonoBehaviour
{
    public Action<Collectible> OnCollectibleDetected;
}