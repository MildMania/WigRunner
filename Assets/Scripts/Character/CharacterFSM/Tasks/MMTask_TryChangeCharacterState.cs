using System;
using System.Linq;
using MMFramework.TasksV2;
using UnityEngine;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;

public class MMTask_TryChangeCharacterState : MMTask
{
    [Serializable]
    private class CharacterStateTransitionInfo
    {
        [SerializeField] private EState _currentState = EState.None;
        public EState CurrentState => _currentState;
    
        [SerializeField] private ETransition _targetTransition = ETransition.None;
        public ETransition TargetTransition => _targetTransition;
    }
    
    [SerializeField] private CharacterFSM _characterFSM = null;

    [SerializeField] private CharacterStateTransitionInfo[] _characterStateTransitionInfoColl = null;
    
    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        CharacterStateTransitionInfo info = _characterStateTransitionInfoColl.FirstOrDefault(
                i => i.CurrentState.Equals(_characterFSM.CurState.StateID));

        if (info == null)
        {
            return ETaskStatus.Completed;
        }
        
        _characterFSM.SetTransition(info.TargetTransition);

        return ETaskStatus.Completed;
    }
}