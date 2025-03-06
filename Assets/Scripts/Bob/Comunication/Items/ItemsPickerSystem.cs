using System;
using Bob.Comunication.Raycating;
using Communication.ManipulatableObjects;
using UnityEngine;
using Utiles.EventSystem;
using Utiles.EventSystem.EventTypes;

namespace Bob.Comunication.Items
{
    public class ItemsPickerSystem : MonoBehaviour
    {
        public event Action<IBaseItem> OnPickItem;
        
        [SerializeField] private int _maxStorageCapacity;
        
        private IBaseItem _pickableInViewRange;
        
        private EventBus _eventBus;
        
        private ItemsStorage<IBaseItem> _itemsStorage;

        [Space, SerializeField] private Transform _root;
        [SerializeField] private Transform _itemRoot;

        [Space, SerializeField] private DirectionalRaycaster _forwardRaycaster;

        public void Initialize(EventBus eventBus, ItemsStorage<IBaseItem> itemsStorage)
        {
            _eventBus = eventBus;
            _itemsStorage = itemsStorage;
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
            if (_pickableInViewRange is not null)
            {
                _pickableInViewRange = null;
                
                var action = new CommunicationStateEvent((object)this, EventPriority.Medium, CommunicationEventType.Remove, PickUp);
                
                _eventBus.Publish(action);
            }
        }

        private void HandleForwardHit(RaycastHit hitInfo)
        {
            if (!hitInfo.collider.TryGetComponent<IBaseItem>(out var pickable))
            {
                HandleStopHitting();

                return;
            }

            if (_itemsStorage.IsFull)
            {
                return;
            }

            if (_pickableInViewRange != pickable)
            {
                _pickableInViewRange = pickable;

                var action = new CommunicationStateEvent((object)this, EventPriority.Medium, CommunicationEventType.Initialize, PickUp);
            
                _eventBus.Publish<CommunicationStateEvent>(action);   
            }
        }

        private void PickUp()
        {
            if (_pickableInViewRange is null)
            {
                return;
            }

            _pickableInViewRange.PickUp(_itemRoot);
            _itemsStorage.TryAddItem(_pickableInViewRange);
            
            OnPickItem?.Invoke(_pickableInViewRange);

            _pickableInViewRange = null;
        }
    }
}