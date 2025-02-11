using System;
using Comunication.ComunicatableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bob.Comunication
{
    public class BobCommunicationController : MonoBehaviour
    {
        public event Action OnCommunicationStart;
        public event Action OnCommunicationEnd;
        public event Action OnReachCommunicatable;
        public event Action OnLostCommunicatable;
        
        private bool _isInProcess;
        private bool _isCommunicationAvailable;

        private ICommunicatable _currentCommunicatable;

        private BaseInput _input;
        
        public void Initialize(BaseInput input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            if (_input is not null)
            {
                _input.Controls.Communication.performed += HandleCommunicationAction;
            }
        }

        private void OnDisable()
        {
            if (_input is not null)
            {
                _input.Controls.Communication.performed -= HandleCommunicationAction;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICommunicatable>(out var communicatable))
            {
                _isCommunicationAvailable = true;
                _currentCommunicatable = communicatable;
                
                OnReachCommunicatable?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ICommunicatable>(out var communicatable))
            {
                _isCommunicationAvailable = false;
                _currentCommunicatable = null;
                
                OnLostCommunicatable?.Invoke();
            }
        }

        private void HandleCommunicationAction(InputAction.CallbackContext obj)
        {
            if (_isInProcess)
            {
                StopCommunicationWithWorkSpace();
            }
            else
            {
                StartCommunicationWithWorkSpace();
            }
        }

        private void StartCommunicationWithWorkSpace()
        {
            OnCommunicationStart?.Invoke();
        }

        private void StopCommunicationWithWorkSpace()
        {
            OnCommunicationEnd?.Invoke();
        }
    }
}
