using Bob.Comunication;
using Bob.Comunication.Items;
using Bob.Controls;
using Bob.View.Rigging;
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

        [field: SerializeField, Header("View")] public BobRiggingController BobRiggingController { get; private set; }

        private BobActionsController _communicationActionController;

        [Space, SerializeField] private BacklightableChecker _backlightableChecker;

        [Space, SerializeField] private ItemsSystemController _itemsSystemController;

        [Space, SerializeField] private BobSetup _setup;

        [Inject]
        private void Initialize(BaseInput input, EventBus eventBus)
        {
            BobMovement.Initialize(input, _setup);
            BobJump.Initialize(input, _setup);
            BobRotator.Initialize(input, _setup);
            
            _itemsSystemController.Initialize(eventBus);

            _communicationActionController = new BobActionsController(input.Controls.Communication, eventBus);
        }

        private void OnDestroy()
        {
            _communicationActionController.Dispose();
        }

        private void HandleCommunicationStart()
        {
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