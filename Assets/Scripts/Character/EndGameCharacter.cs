using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class EndGameCharacter : Singleton<EndGameCharacter>
{
    [SerializeField] private EndGameCharacterFSM _fsm;
    public EndGameCharacterFSM FSM => _fsm;

    public Transform WigAttachPoint;

    public EndGameCharacterFirstWalkState FirstWalkState;
    public EndGameCharacterObtainWigState ObtainWigState;
    public EndGameCharacterPosingState PosingState;
}
