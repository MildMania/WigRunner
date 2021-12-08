using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private NpcFSM _npcFSM;
    

    public void Activate(ECrowdJoy crowdJoy)
    {
        if (crowdJoy.Equals(ECrowdJoy.Low))
        {
            _npcFSM.SetTransition(NpcFSMController.ETransition.LowJoy);
        }

        else if (crowdJoy.Equals(ECrowdJoy.Middle))
        {
            _npcFSM.SetTransition(NpcFSMController.ETransition.MiddleJoy);
        }

        else if (crowdJoy.Equals(ECrowdJoy.High))
        {
            _npcFSM.SetTransition(NpcFSMController.ETransition.HighJoy);
        }

    }
    
}
