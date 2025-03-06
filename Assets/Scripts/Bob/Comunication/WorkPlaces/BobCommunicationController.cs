using System;
using Communication.ComunicatableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Utiles.EventSystem;
using Utiles.EventSystem.EventTypes;

namespace Bob.Comunication
{
    public class BobCommunicationController : MonoBehaviour
    {
        public event Action OnCommunicationStart;
        public event Action OnCommunicationEnd;
        public event Action OnReachCommunicatable;
        public event Action OnLostCommunicatable;
        
        private bool _isInProcess;

        private ICommunicatable _currentCommunicatable;

        private EventBus _eventBus;
        
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICommunicatable>(out var communicatable))
            {
                _currentCommunicatable = communicatable;
                
                OnReachCommunicatable?.Invoke();

                var action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Initialize, StartCommunicationWithWorkSpace);
                
                _eventBus.Publish<CommunicationStateEvent>(action);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ICommunicatable>(out var communicatable))
            {
                _currentCommunicatable = null;
                
                OnLostCommunicatable?.Invoke();
                
                var action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Remove, StartCommunicationWithWorkSpace);
                
                _eventBus.Publish<CommunicationStateEvent>(action);
            }
        }

        private void StartCommunicationWithWorkSpace()
        {
            OnCommunicationStart?.Invoke();

            _isInProcess = true;
            
            var action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Remove, StartCommunicationWithWorkSpace);
            
            _eventBus.Publish(action);

            action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Initialize, StopCommunicationWithWorkSpace);
            
            _eventBus.Publish(action);
        }

        private void StopCommunicationWithWorkSpace()
        {
            OnCommunicationEnd?.Invoke();

            _isInProcess = false;
            
            var action = new CommunicationStateEvent((object)this, EventPriority.High, CommunicationEventType.Remove, StopCommunicationWithWorkSpace);
            
            _eventBus.Publish(action);
        }
    }
}
