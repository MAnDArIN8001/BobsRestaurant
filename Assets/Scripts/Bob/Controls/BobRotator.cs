using System;
using Setups.Bob;
using UnityEngine;
using Utiles;
using Zenject;

namespace Bob.Controls
{
    public class BobRotator : MonoBehaviour, IStatable
    {
        public event Action<Vector2> OnRotationCompute;

        private bool _enabled;
        
        private float _currentCameraRotationAngel;

        [SerializeField] private Utiles.Range _viewRange;

        [Space, SerializeField] private Transform _cameraRoot;

        private Vector2 _lastInput;
        
        private BaseInput _input;

        private BobSetup _setup;

        public void Initialize(BaseInput input, BobSetup setup)
        {
            _input = input;
            _setup = setup;
            
            _enabled = true;
        }

        private void Update()
        {
            if (!_enabled)
            {
                return;
            }
            
            var inputValues = ReadInputValues();

            if (inputValues != _lastInput)
            {
                OnRotationCompute?.Invoke(inputValues);
            }
            
            RotateHorizontal(inputValues);
            RotateVertical(inputValues);
            
            _lastInput = inputValues;
        }

        private void RotateHorizontal(Vector2 input)
        {
            var sensitivity = _setup.HorizontalSensitivity;

            float targetYRotation = transform.eulerAngles.y + input.x * sensitivity;
            float smoothedYRotation = Mathf.LerpAngle(transform.eulerAngles.y, targetYRotation, Time.deltaTime * 10f);
                
            transform.rotation = Quaternion.Euler(0, smoothedYRotation, 0);
        }

        private void RotateVertical(Vector2 input)
        {
            var sensitivity = _setup.VerticalSensitivity;
            
            _currentCameraRotationAngel += (-input.y) * sensitivity * Time.deltaTime;
            _currentCameraRotationAngel = Mathf.Clamp(_currentCameraRotationAngel, _viewRange.MinValue, _viewRange.MaxValue);

            _cameraRoot.localRotation = Quaternion.Euler(_currentCameraRotationAngel, 0, 0);
        }
        
        private Vector2 ReadInputValues() => _input.Mouse.Delta.ReadValue<Vector2>();
        
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