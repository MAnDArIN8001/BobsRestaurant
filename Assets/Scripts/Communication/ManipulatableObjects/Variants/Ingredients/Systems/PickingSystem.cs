using System;
using UnityEngine;

namespace Communication.ManipulatableObjects.Variants.Ingredients
{
    public class PickingSystem : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        [SerializeField] private Transform _root;

        [Space, SerializeField] private ItemMovingView _movingView;

        private void Awake()
        {
            if (!_root.gameObject.TryGetComponent<Rigidbody>(out _rigidbody))
            {
                Debug.LogWarning($"The object {_root.gameObject} doesnt contains the Rigidbody Component");
            }
        }
        
        public void PickUp(Transform newRoot)
        {
            _root.SetParent(newRoot);

            _movingView.TeleportToPoint(newRoot);
            
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        public void Drop()
        {
            _root.SetParent(null);
            
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }
    }
}