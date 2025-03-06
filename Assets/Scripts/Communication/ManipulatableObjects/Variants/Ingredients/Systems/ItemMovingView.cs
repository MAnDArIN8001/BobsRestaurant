using DG.Tweening;
using UnityEngine;

namespace Communication.ManipulatableObjects.Variants.Ingredients
{
    public class ItemMovingView : MonoBehaviour
    {
        [Header("Animation")] 
        [SerializeField] private float _scalingTime;

        [Space, SerializeField] private Ease _scalingEase;

        [Space, SerializeField] private Transform _root;

        private Vector3 _defaultScale;
        
        private Tween _scalingTween;

        private void OnDestroy()
        {
            if (_scalingTween is not null && _scalingTween.active)
            {
                _scalingTween.Kill();
            }
        }

        public void TeleportToPoint(Transform point)
        {
            _defaultScale = _root.localScale;
            _scalingTween = _root.DOScale(Vector3.zero, _scalingTime).SetEase(_scalingEase).OnComplete(() => UpdatePosition(point.position));
        }

        private void UpdatePosition(Vector3 newPosition)
        {
            _root.position = newPosition;
            _scalingTween = _root.DOScale(_defaultScale, _scalingTime).SetEase(_scalingEase);
        }
    }
}