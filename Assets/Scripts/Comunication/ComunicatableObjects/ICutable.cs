using System;

namespace Comunication.ComunicatableObjects
{
    public interface ICutable
    {
        public event Action<float> OnCUtProgressChanged;
        
        public void Cut();
    }
}