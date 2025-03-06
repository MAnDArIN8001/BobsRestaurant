using System;
using Communication.ManipulatableObjects;
using UnityEngine;
using Utiles.EventSystem;
using Utiles.EventSystem.EventTypes;

namespace Bob.Comunication.Items
{
    public class ItemsDropingSystem : MonoBehaviour
    {
        public event Action OnDropItem;
        
        private IBaseItem _lastItemInHand;
        
        private EventBus _eventBus;

        private ItemsStorage<IBaseItem> _itemsStorage;

        public void Initialize(EventBus eventBus, ItemsStorage<IBaseItem> itemsStorage)
        {
            _eventBus = eventBus;
            _itemsStorage = itemsStorage;
        }

        public void OnEnable()
        {
            if (_itemsStorage is not null)
            {
                _itemsStorage.OnReceiveItem += HandleNewItem;
                _itemsStorage.OnDropItem += HandleLostItem;
            }
        }

        public void OnDisable()
        {
            if (_itemsStorage is not null)
            {
                _itemsStorage.OnReceiveItem -= HandleNewItem;
                _itemsStorage.OnDropItem -= HandleLostItem;
            }
        }

        private void HandleNewItem(IBaseItem item)
        {
            if (_lastItemInHand is null)
            {
                var action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Initialize, DropItem);
                
                _eventBus.Publish(action);
            }
            
            _lastItemInHand = item;
        }

        private void HandleLostItem(IBaseItem item)
        {
            if (!_itemsStorage.TryTakeLast(out var lastItem))
            {
                _lastItemInHand = null;
                
                var action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Remove, DropItem);
                
                _eventBus.Publish(action);
                
                return;
            }

            _lastItemInHand = lastItem;
        }

        private void DropItem()
        {
            OnDropItem?.Invoke();
            
            _lastItemInHand.Drop();
            _itemsStorage.TryRemoveItem(_lastItemInHand);
        }
    }
}