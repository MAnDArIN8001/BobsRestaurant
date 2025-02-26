using System;
using Bob.Comunication;
using Bob.Comunication.Items;
using Bob.Controls;
using Setups.Bob;
using UnityEngine;
using Utiles.EventSystem;
using Zenject;

namespace Bob
{
    public class Bob : MonoBehaviour
    {
        [field: SerializeField] public BobMovement BobMovement { get; private set; }
        [field: SerializeField] public BobJump BobJump { get; private set; }
        [field: SerializeField] public BobRotator BobRotator { get; private set; }
        [field: SerializeField] public BobCommunicationController BobCommunicationController { get; private set; }

        [Header("Inner Systems")] 
        [SerializeField] private ItemsPickerSystem _itemsPickerSystem;

        private BobActionsController _communicationActionController;

        [SerializeField, Space] private BacklightableChecker _backlightableChecker;

        [Space, SerializeField] private BobSetup _setup;

        [Inject]
        private void Initialize(BaseInput input, EventBus eventBus)
        {
            BobMovement.Initialize(input, _setup);
            BobJump.Initialize(input, _setup);
            BobRotator.Initialize(input, _setup);

            BobCommunicationController.Initialize(eventBus);

            _communicationActionController = new BobActionsController(input.Controls.Communication, eventBus);
        }

        private void OnEnable()
        {
            if (BobCommunicationController is not null)
            {
                BobCommunicationController.OnCommunicationStart += HandleCommunicationStart;
                BobCommunicationController.OnCommunicationEnd += HandleCommunicationEnd;
            }
        }

        private void OnDisable()
        {
            if (BobCommunicationController is not null)
            {
                BobCommunicationController.OnCommunicationStart -= HandleCommunicationStart;
                BobCommunicationController.OnCommunicationEnd -= HandleCommunicationEnd;
            }
        }

        private void HandleCommunicationStart()
        {
            Debug.Log("Communication start");
            BobJump.Disable();
            BobMovement.Disable();
            BobRotator.Disable();
            
            _backlightableChecker.Disable();
        }

        private void HandleCommunicationEnd()
        {
            BobJump.Enable();
            BobMovement.Enable();
            BobRotator.Enable();
            
            _backlightableChecker.Enable();
        }
    }
}