using System;
using UnityEngine;

namespace WorkSpace.Systems.Trigger
{
    public class TriggerSystem : MonoBehaviour
    {
        public event Action<Collider> OnTriggersEnter;
        public event Action<Collider> OnTriggersStay;
        public event Action<Collider> OnTriggersExit;

        public void OnTriggerEnter(Collider other)
        {
            OnTriggersEnter?.Invoke(other);
        }

        public void OnTriggerStay(Collider other)
        {
            OnTriggersStay?.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            OnTriggersExit?.Invoke(other);
        }
    }
}