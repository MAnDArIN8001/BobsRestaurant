using System;

namespace Utiles.EventSystem.EventTypes
{
    public struct CommunicationStateEvent
    {
        public object Sender { get; }
        
        public EventPriority EventPriority { get; }
        public CommunicationEventType Type { get; }

        public Action CallBack { get; }

        public CommunicationStateEvent(object sender, EventPriority priority, CommunicationEventType type, Action callBack)
        {
            Sender = sender;
            CallBack = callBack;
            EventPriority = priority;
            Type = type;
        }
    }
}