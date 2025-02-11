using System;
using Bob.Comunication;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Zenject;

namespace UI.Communicatable
{
    public class CommunicationView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _communicationText;

        private InputTypeDetector _inputTypeDetector;

        [Inject]
        private void Initialize(InputTypeDetector typeDetector)
        {
            _inputTypeDetector = typeDetector;
        }

        private void OnEnable()
        {
            if (_inputTypeDetector is not null)
            {
                _inputTypeDetector.OnInputTypeChanged += UpdateUIWithInputType;
            }
        }

        private void OnDisable()
        {
            if (_inputTypeDetector is not null)
            {
                _inputTypeDetector.OnInputTypeChanged -= UpdateUIWithInputType;
            }
        }

        private void UpdateUIWithInputType(InputType inputType)
        {
            string requiredButton = "";
        }
    }
}