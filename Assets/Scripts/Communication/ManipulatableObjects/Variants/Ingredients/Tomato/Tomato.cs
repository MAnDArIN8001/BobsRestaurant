using UnityEngine;

namespace Communication.ManipulatableObjects.Variants.Ingredients.Tomato
{
    public class Tomato : MonoBehaviour, IBaseCutableItem
    {
        [SerializeField] private PickingSystem _pickingSystem;
        [SerializeField] private CuttingSystem _cuttingSystem;

        public void PickUp(Transform newRoot)
        {
            _pickingSystem.PickUp(newRoot);
        }

        public void Drop()
        {
            _pickingSystem.Drop();
        }

        public void Cut()
        {
            _cuttingSystem.Cut();
        }
    }
}