using Bob.Comunication.Items;
using Communication.ManipulatableObjects.Variants;
using UnityEngine;
using WorkSpace.FSM.State;
using WorkSpace.Systems.Trigger;

namespace WorkSpace.CutingTable.CuttingTableFSM.States
{
    public class CuttingTableIdle : State
    {
        private readonly Transform _root;
        
        private readonly TriggerSystem _triggerSystem;

        private readonly ItemsStorage<IBaseCutableItem> _itemsStorage;
        
        public CuttingTableIdle(Transform root, TriggerSystem triggerSystem, ItemsStorage<IBaseCutableItem> itemsStorage)
        {
            _root = root;
            _triggerSystem = triggerSystem;
            _itemsStorage = itemsStorage;

            StateType = BehaviourState.Idle;
        }

        public override void Enter()
        {
            if (_triggerSystem is not null)
            {
                _triggerSystem.OnTriggersEnter += HandleTriggerEnter;
            }
        }

        public override void Update() { }

        public override void Exit()
        {
            if (_triggerSystem is not null)
            {
                _triggerSystem.OnTriggersEnter -= HandleTriggerEnter;
            }
        }

        private void HandleTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IBaseCutableItem>(out var item))
            {
                if (_itemsStorage.IsFull)
                {
                    return;
                }
                
                item.PickUp(_root);

                _itemsStorage.TryAddItem(item);
            }
        }
    }
}