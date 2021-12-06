using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBase : MonoBehaviour
{


    private void OnTriggerStay(Collider other)
    {
        OnStayCustomActions();
    }

    protected virtual void OnStayCustomActions()
    {

    }
}
