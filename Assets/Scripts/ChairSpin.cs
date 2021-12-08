using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class ChairSpin : MonoBehaviour
{
    [SerializeField] private GameObject _model;


    private void Awake()
    {
        EndGameCharacter.Instance.ObtainWigState.OnWigObtained += Spin;
    }


    private void Spin(float duration)
    {
        _model.transform.DORotateQuaternion(Quaternion.LookRotation(-Vector3.forward), duration);
    }

}
