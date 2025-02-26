using Bob.Comunication.Raycating;
using Comunication.Pickable;
using UnityEngine;
using Utiles.EventSystem;
using Utiles.EventSystem.EventTypes;

namespace Bob.Comunication.Items
{
    public class ItemsPickerSystem : MonoBehaviour
    {
        [SerializeField] private int _maxStorageCapacity;
        
        private IPickable _pickableInViewRange;
        
        private EventBus _eventBus;
        
        private ItemsStorage<IPickable> _itemsStorage;

        [Space, SerializeField] private DirectionalRaycaster _forwardRaycaster;

        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            if (_forwardRaycaster is not null)
            {
                _forwardRaycaster.OnHitObject += HandleForwardHit;
                _forwardRaycaster.OnStopHitting += HandleStopHitting;
            }
        }

        private void OnDisable()
        {
            if (_forwardRaycaster is not null)
            {
                _forwardRaycaster.OnHitObject -= HandleForwardHit;
                _forwardRaycaster.OnStopHitting -= HandleStopHitting;
            }
        }

        private void HandleStopHitting()
        {
            
        }

        private void HandleForwardHit(RaycastHit hitInfo)
        {
            if (!hitInfo.collider.TryGetComponent<IPickable>(out var pickable))
            {
                return;
            }

            if (_pickableInViewRange != pickable)
            {
                _pickableInViewRange = pickable;
            }

            if (_itemsStorage.IsFull)
            {
                return;
            }

            var action = new CommunicationStateUpdateEvent((object)this, EventPriority.Medium, CommunicationEventType.Initialize, PickUp);
        }

        private void PickUp()
        {
            if (_pickableInViewRange is null)
            {
                return;
            }
            
            _pickableInViewRange.PickUp(transform);
        }
    }
}