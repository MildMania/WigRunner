using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterBase : MonoBehaviour
{
    [SerializeField] private GameObject _raySource;

    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _rayLength = 5f;
    [SerializeField] private float _thickness = 1f;

    protected bool _isCharacterEntered = false;


    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!_isCharacterEntered)
        {
            Vector3 dir = -_raySource.transform.up;
            Ray ray = new Ray(_raySource.transform.position, dir);

            if (Physics.SphereCast(ray, _thickness, out var raycastHit, _rayLength, _targetLayer))
            {
                OnCharacterEntered();

                _isCharacterEntered = true;
            }
        }
    }


    protected virtual void OnCharacterEntered()
    {
        print("Character Entered");
    }
}
