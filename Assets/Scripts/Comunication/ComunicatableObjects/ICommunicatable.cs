using Rider;
using UnityEngine;

namespace Comunication.ComunicatableObjects
{
    public interface ICommunicatable
    {
        public Transform HandPosition { get; }
        public Transform Root { get; }
    }
}
