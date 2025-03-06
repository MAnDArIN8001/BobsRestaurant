using System;

namespace Communication.ComunicatableObjects
{
    public interface ICommunicatable
    {
        public event Action OnCommunicationStart;
        public event Action OnCommunicationEnd;
        
        public void StartCommunication();
        public void StopCommunication();
    }
}
