using Bob.Comunication;
using DG.Tweening;
using UnityEngine;
using TMPro;
using Utiles.Services.InputViewService;
using Zenject;
using InputActionType = Utiles.InputActionType;

namespace UI.Communicatable
{
    public class CommunicationView : MonoBehaviour
    {
        [SerializeField] private float _animatingTime;
        
        [SerializeField] private TMP_Text _communicationText;

        private BobCommunicationController _bobCommunication;

        private InputTypeDetector _inputTypeDetector;

        private InputViewService _inputViewService;

        private Tween _fadeTween;

        [Inject]
        private void Initialize(InputTypeDetector typeDetector, InputViewService inputViewService, Bob.Bob bob)
        {
            _inputTypeDetector = typeDetector;
            _inputViewService = inputViewService;

            _bobCommunication = bob.BobCommunicationController;
        }

        private void Start()
        {
            var currentInputDevice = _inputTypeDetector.CurrentInputType;
            
            UpdateUIWithInputType(currentInputDevice);
        }

        private void OnEnable()
        {
            if (_bobCommunication is not null)
            {
                _bobCommunication.OnReachCommunicatable += Enable;
                _bobCommunication.OnLostCommunicatable += Disable;
            }
            
            if (_inputTypeDetector is not null)
            {
                _inputTypeDetector.OnInputTypeChanged += UpdateUIWithInputType;
            }
        }

        private void OnDisable()
        {
            if (_bobCommunication is not null)
            {
                _bobCommunication.OnReachCommunicatable -= Enable;
                _bobCommunication.OnLostCommunicatable -= Disable;
            }
            
            if (_inputTypeDetector is not null)
            {
                _inputTypeDetector.OnInputTypeChanged -= UpdateUIWithInputType;
            }
        }

        private void Enable()
        {
            if (_fadeTween is not null && _fadeTween.active)
            {
                _fadeTween.Kill();
            }
            
            _fadeTween = _communicationText.DOFade(1, _animatingTime);
        }

        private void Disable()
        {
            if (_fadeTween is not null && _fadeTween.active)
            {
                _fadeTween.Kill();
            }
            
            _fadeTween = _communicationText.DOFade(0, _animatingTime);
        }

        private void UpdateUIWithInputType(InputType inputType)
        {
            var key = _inputViewService.GetKeyNameForActionOnDevice(InputActionType.Communication, inputType);

            _communicationText.text = $"Press {key}";
        }
    }
}