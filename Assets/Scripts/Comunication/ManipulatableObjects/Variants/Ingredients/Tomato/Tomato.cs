using Comunication.Pickable;
using Comunication.Pickable.Variants.Ingredients;
using UnityEngine;

namespace Comunication.ManipulatableObjects.Variants.Ingredients.Tomato
{
    [RequireComponent(typeof(Rigidbody))]
    public class Tomato : MonoBehaviour, IPickable, IDropable
    {
        private Rigidbody _rigidbody;
        
        [SerializeField] private ItemMovingView _movingView;

        private void Start()
        {
            if (!gameObject.TryGetComponent<Rigidbody>(out _rigidbody))
            {
                Debug.LogWarning("The object tomato doesnt contains the Rigidbody Component");
            }
        }

        public void PickUp(Transform newRoot)
        {
            transform.SetParent(newRoot, false);
            
            _movingView.TeleportToPoint(newRoot);
            _rigidbody.useGravity = false;
        }

        public void Drop()
        {
            transform.SetParent(null);
            
            _rigidbody.useGravity = true;
        }
    }
}