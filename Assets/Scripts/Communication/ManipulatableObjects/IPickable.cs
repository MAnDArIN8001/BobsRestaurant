using UnityEngine;

namespace Communication.ManipulatableObjects
{
    public interface IPickable
    {
        public void PickUp(Transform newRoot);
    }
}