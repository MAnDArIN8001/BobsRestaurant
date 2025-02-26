using UnityEngine;
using DG.Tweening;

namespace Comunication.Pickable.Variants.Ingredients
{
    public class ItemMovingView : MonoBehaviour
    {
        [Header("Animation")] 
        [SerializeField] private float _scalingTime;

        [Space, SerializeField] private Ease _scalingEase;

        private Vector3 _defaultScale;
        
        [Header("Effect")]
        [SerializeField] private GameObject _movingEffect;

        private Tween _scalingTween;

        private void Start()
        {
            _defaultScale = transform.localScale;
        }

        private void OnDestroy()
        {
            if (_scalingTween is not null && _scalingTween.active)
            {
                _scalingTween.Kill();
            }
        }

        public void TeleportToPoint(Transform point)
        {
            _scalingTween = transform.DOScale(Vector3.zero, _scalingTime).SetEase(_scalingEase).OnComplete(() => UpdatePosition(point.position));
            
            Instantiate(_movingEffect, transform.position, Quaternion.identity);
        }

        private void UpdatePosition(Vector3 newPosition)
        {
            _scalingTween = transform.DOScale(_defaultScale, _scalingTime).SetEase(_scalingEase);
        }
    }
}