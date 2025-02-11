using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bob.Comunication
{
    public class InputTypeDetector : MonoBehaviour
    {
        public event Action<InputType> OnInputTypeChanged;
        
        public InputType CurrentInputType { get; private set; }
            
        [SerializeField, Range(1, 60)] private int _offsetBetweenChecks;
        private int _currentOffsetCounter;

        private void Update()
        {
            if (_currentOffsetCounter >= _offsetBetweenChecks)
            {
                _currentOffsetCounter = 0;
                
                CheckForCurrentDevice();
            }
            
            _currentOffsetCounter++;
        }

        private void CheckForCurrentDevice()
        {
            if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
            {
                SetInputType(InputType.Gamepad);
            }

            if (Keyboard.current.wasUpdatedThisFrame || Mouse.current.wasUpdatedThisFrame)
            {
                SetInputType(InputType.KeyboardAndMouse);
            }
        }

        private void SetInputType(InputType inputType)
        {
            if (CurrentInputType != inputType)
            {
                CurrentInputType = inputType;
                
                OnInputTypeChanged?.Invoke(inputType);
            }
        }
    }
}