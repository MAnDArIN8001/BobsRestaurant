using System;
using UnityEngine;

namespace Bob.Comunication.Raycating
{
    public class DirectionalRaycaster : MonoBehaviour
    {
        public event Action<RaycastHit> OnHitObject;
        public event Action OnStopHitting;

        private bool _isLastFrameWasHitted;
        
        [SerializeField] private float _checkDistance;
        
        [Space, SerializeField, Range(1, 60)] private int _checkOffset;
        private int _currentOffset;

        [Space, SerializeField] private Transform _checkPoint;

        public RaycastHit HitInfo { get; private set; }

        private void Update()
        {
            _currentOffset++;
            
            if (_currentOffset >= _checkOffset)
            {
                _currentOffset = 0;
                
                Debug.DrawRay(_checkPoint.position, _checkPoint.forward*_checkDistance, Color.green);

                if (Physics.Raycast(_checkPoint.position, _checkPoint.forward, out var hitInfo, _checkDistance))
                {
                    OnHitObject?.Invoke(hitInfo);

                    _isLastFrameWasHitted = true;
                }
                else if (_isLastFrameWasHitted)
                {
                    HitInfo = default;
                    _isLastFrameWasHitted = false;
                    
                    OnStopHitting?.Invoke();
                }
            }
        }
    }
}