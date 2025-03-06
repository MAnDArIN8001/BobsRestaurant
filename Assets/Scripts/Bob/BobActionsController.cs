using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Utiles.EventSystem;
using Utiles.EventSystem.EventTypes;
using Debug = UnityEngine.Debug;

namespace Bob
{
    public class BobActionsController : IDisposable
    {
        private readonly InputAction _inputAction;

        private readonly EventBus _eventBus;

        private List<CommunicationStateEvent> _actions;

        public BobActionsController(InputAction input, EventBus eventBus)
        {
            _inputAction = input;
            _eventBus = eventBus;

            _actions = new List<CommunicationStateEvent>();
            
            _eventBus.Subscribe<CommunicationStateEvent>(HandleAction);

            _inputAction.performed += HandleInputAction;
        }

        private void HandleInputAction(InputAction.CallbackContext context)
        {
            if (_actions.Count == 0)
            {
                return;
            }
            
            var actionToCall = _actions.First();

            actionToCall.CallBack?.Invoke();
        }

        private void HandleAction(CommunicationStateEvent action)
        {
            switch (action.Type)
            {
                case CommunicationEventType.Initialize:
                    InitializeNewAction(action);
                    break;
                
                case CommunicationEventType.Remove:
                    RemoveAction(action);
                    break;
                
                default:
                    Debug.LogWarning($"Unknow update type for action {action}");
                    break;
            }
        }

        private void InitializeNewAction(CommunicationStateEvent action)
        {
            if (_actions.Any(item => item.CallBack == action.CallBack))
            {
                Debug.LogWarning($"The Actions Controller already contains action {action}");

                return;
            }

            _actions.Add(action);

            _actions.Sort((action, nextAction) => nextAction.EventPriority.CompareTo(action.EventPriority));
        }

        private void RemoveAction(CommunicationStateEvent action)
        {
            var actionToRemove = _actions.FirstOrDefault(item => item.CallBack == action.CallBack);
            
            if (actionToRemove.CallBack is null)
            {
                Debug.LogWarning($"The Action Controller doesnt contains action {action}");

                return;
            }

            _actions.Remove(actionToRemove);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<CommunicationStateEvent>(HandleAction);

            _inputAction.performed -= HandleInputAction;
            
            _actions.Clear();
        }
    }
}