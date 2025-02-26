using System;
using Comunication.ComunicatableObjects;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BobRiggingController : MonoBehaviour
{
    private bool _isRightHandEnabled;
    private bool _isLeftHandEnabled;
    
    [Header("Right Hand")] 
    [SerializeField] private Transform _rightHandIKTarget;
    [SerializeField] private TwoBoneIKConstraint _rightHandConstraint;
    
    private Transform _rightHandPosition;

    [Space, Header("Left Hand")] 
    [SerializeField] private Transform _leftHandIKTarget;
    [SerializeField] private TwoBoneIKConstraint _leftHandConstraint;

    private Transform _leftHandPosition;

    [Space, Header("Root For Comunicatables")] 
    [SerializeField] private Transform _rootForComunicatablees;

    private void Update()
    {
        if (_rightHandPosition is not null)
        {
            _rightHandConstraint.weight = 1f;
            _rightHandIKTarget.position = _rightHandPosition.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICommunicatable>(out var comunicatable))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == _rightHandPosition)
        {
            _rightHandPosition = null;
            _rightHandConstraint.weight = 0f;
        }
    }
}
