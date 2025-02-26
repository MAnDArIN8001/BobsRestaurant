using UnityEngine;

namespace Comunication.Pickable
{
    public interface IPickable
    {
        public void PickUp(Transform newRoot);
    }
}