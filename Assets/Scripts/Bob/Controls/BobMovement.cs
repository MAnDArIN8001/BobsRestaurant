using System;
using Setups.Bob;
using TMPro;
using UnityEngine;
using Utiles;
using Zenject;

namespace Bob.Controls
{
    [RequireComponent(typeof(Rigidbody))]
    public class BobMovement : MonoBehaviour, IStatable
    {
        public event Action<Vector2> OnMovementCompute;

        private bool _enabled;

        private Vector2 _lastInput;
        
        private Rigidbody _rigidbody;
        
        private BaseInput _input;

        private BobSetup _setup;

        public void Initialize(BaseInput input, BobSetup setup)
        {
            _input = input;
            _setup = setup;

            _enabled = true;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_enabled)
            {
                return;
            }
            
            Vector2 input = ReadMovementInput();
            
            Move(input);

            if (_lastInput != input)
            {
                OnMovementCompute?.Invoke(input);
            }

            _lastInput = input;
        }

        private void Move(Vector2 input)
        {
            Vector3 movementVelocity = ComputeInputOnMovement(input);
            
            movementVelocity *= _setup.MovementSpeed;
            movementVelocity.y = _rigidbody.linearVelocity.y;

            _rigidbody.linearVelocity = movementVelocity;
        }

        private Vector3 ComputeInputOnMovement(Vector2 input)
        {
            var movementVelocity = transform.forward * input.y + transform.right * input.x;

            return movementVelocity.normalized;
        }

        private Vector2 ReadMovementInput() => _input.Controls.Movement.ReadValue<Vector2>();
        
        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }
    }
}
