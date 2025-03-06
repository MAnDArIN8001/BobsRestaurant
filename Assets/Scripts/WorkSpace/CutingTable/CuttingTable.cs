using System;
using System.Collections.Generic;
using Bob.Comunication.Items;
using Communication.ManipulatableObjects.Variants;
using Setups.WorkSpace;
using UnityEngine;
using UnityEngine.Serialization;
using WorkSpace.CutingTable.CuttingTableFSM;
using WorkSpace.CutingTable.CuttingTableFSM.States;
using WorkSpace.FSM;
using WorkSpace.FSM.State;
using WorkSpace.Systems.Trigger;
using WorkSpace.View;

namespace WorkSpace.CutingTable
{
   public class CuttingTable : MonoBehaviour
   {
      [SerializeField] private int _storageCapacity;

      [Space, SerializeField] private Transform _root;

      [Header("Systems")] 
      [SerializeField] private TriggerSystem _triggerSystem;
      
      [Space, SerializeField] private ProcessingView _processingView;
      [SerializeField] private ProcessStateView _processStateView;

      [Header("Setups")] 
      [SerializeField] private WorkSpaceSetup _workspaceSetup;

      private ItemsStorage<IBaseCutableItem> _itemsStorage;

      public StateMachine StateMachine { get; private set; }

      private void Awake()
      {
         _itemsStorage = new ItemsStorage<IBaseCutableItem>(_storageCapacity);
         
         var states = new Dictionary<BehaviourState, State>()
         {
            { BehaviourState.Idle, new CuttingTableIdle(_root, _triggerSystem, _itemsStorage) },
            { BehaviourState.InProcess, new CuttingTableProcess(_itemsStorage, _workspaceSetup, _processingView, _processStateView) }
         };
         
         var transition = new List<Transition>()
         {
            new Transition(BehaviourState.Idle, BehaviourState.InProcess, () => _itemsStorage.IsFull),
            new Transition(BehaviourState.InProcess, BehaviourState.Idle, () => !_itemsStorage.IsFull),
         };
         
         StateMachine = new CuttingTableStateMachine(this, states, transition);
         
         StateMachine.SetState(BehaviourState.Idle);
      }

      private void Update()
      {
         StateMachine.Update();
      }

      private void OnDestroy()
      {
         _itemsStorage.Dispose();
      }
   }
}
