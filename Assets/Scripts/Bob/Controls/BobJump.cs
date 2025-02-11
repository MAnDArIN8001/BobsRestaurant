using System;
using Setups.Bob;
using UnityEngine;
using UnityEngine.InputSystem;
using Utiles;

namespace Bob.Controls
{
    [RequireComponent(typeof(Rigidbody))]
    public class BobJump : MonoBehaviour, IStatable
    {
        public event Action<bool> OnGroundedStateChanged;

        private bool _isOnGround;
        private bool _enabled;

        [SerializeField] private float _groundCheckDistance;

        [Space, SerializeField] private Transform _groundChecker;

        private Rigidbody _rigidbody;
        
        private BaseInput _input;

        private BobSetup _setup;
        
        public void Initialize(BaseInput input, BobSetup setup)
        {
            _input = input;
            _setup = setup;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (_input is not null)
            {
                _input.Controls.Jump.started += HandleJump;
            }
        }

        private void OnDisable()
        {
            if (_input is not null)
            {
                _input.Controls.Jump.started -= HandleJump;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!_isOnGround 
                && Physics.Raycast(_groundChecker.position, -transform.up, _groundCheckDistance))
            {
                _isOnGround = true;

                OnGroundedStateChanged?.Invoke(_isOnGround);
            }
        }

        private void HandleJump(InputAction.CallbackContext obj)
        {
            if (!_isOnGround || !_enabled)
            {
                return;
            }

            _isOnGround = false;
            
            Jump();
            
            OnGroundedStateChanged?.Invoke(_isOnGround);
        }

        private void Jump()
        {
            _rigidbody.AddForce(transform.up * _setup.JumpForce, ForceMode.Impulse);

            OnGroundedStateChanged?.Invoke(_isOnGround);
        }
        
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
