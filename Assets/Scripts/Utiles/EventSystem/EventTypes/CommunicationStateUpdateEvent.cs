using System;

namespace Utiles.EventSystem.EventTypes
{
    public struct CommunicationStateUpdateEvent
    {
        public object Sender { get; }
        
        public EventPriority EventPriority { get; }
        public CommunicationEventType Type { get; }

        public Action CallBack { get; }

        public CommunicationStateUpdateEvent(object sender, EventPriority priority, CommunicationEventType type, Action callBack)
        {
            Sender = sender;
            CallBack = callBack;
            EventPriority = priority;
            Type = type;
        }
    }
}