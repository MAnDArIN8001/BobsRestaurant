using Bob.View.Rigging;
using Communication.ManipulatableObjects;
using UnityEngine;
using Utiles.EventSystem;

namespace Bob.Comunication.Items
{
    public class ItemsSystemController : MonoBehaviour
    {
        [SerializeField] private int _maxStorageCapacity;
        
        [Space, SerializeField] private BobRiggingController _riggingController;

        [field: SerializeField, Space] public ItemsPickerSystem ItemsPickerSystem { get; private set; }
        [field: SerializeField] public ItemsDropingSystem ItemsDropingSystem { get; private set; }

        public ItemsStorage<IBaseItem> ItemsStorage { get; private set; }

        public void Initialize(EventBus eventBus)
        {
            ItemsStorage = new ItemsStorage<IBaseItem>(_maxStorageCapacity);
            
            ItemsPickerSystem.Initialize(eventBus, ItemsStorage);
            ItemsDropingSystem.Initialize(eventBus, ItemsStorage);
        }

        private void OnDestroy()
        {
            ItemsStorage.Dispose();
        }
    }
}