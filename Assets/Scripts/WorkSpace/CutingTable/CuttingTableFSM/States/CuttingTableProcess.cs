using Bob.Comunication.Items;
using Communication.ManipulatableObjects.Variants;
using Setups.WorkSpace;
using UnityEngine;
using WorkSpace.FSM.State;
using WorkSpace.View;

namespace WorkSpace.CutingTable.CuttingTableFSM.States
{
    public class CuttingTableProcess : State
    {
        private float _currentTime;

        private readonly ProcessingView _processingView;
        private readonly ProcessStateView _processStateView;
        
        private readonly WorkSpaceSetup _workSpaceSetup;
        
        private readonly ItemsStorage<IBaseCutableItem> _itemsStorage;
        
        public CuttingTableProcess(ItemsStorage<IBaseCutableItem> itemsStorage, 
            WorkSpaceSetup setup, 
            ProcessingView processingView,
            ProcessStateView processStateView)
        {
            _workSpaceSetup = setup;
            _itemsStorage = itemsStorage;
            _processingView = processingView;
            _processStateView = processStateView;

            StateType = BehaviourState.InProcess;
        }
        
        public override void Enter()
        {
            _currentTime = 0;
            
            Debug.Log("Process enter");
            
            _processStateView.ShowUI();
            _processingView.PlayProcessing(_workSpaceSetup.ProcessingTime);
        }

        public override void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _workSpaceSetup.ProcessingTime)
            {
                _currentTime = 0;

                if (_itemsStorage.TryTakeLast(out var item))
                {
                    _itemsStorage.TryRemoveItem(item);
                    
                    item.Cut();
                }
            }
        }

        public override void Exit()
        {
            _processStateView.HideUI();
        }
    }
}