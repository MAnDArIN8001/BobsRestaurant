using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorkSpace.View
{
    public class ProcessStateView : MonoBehaviour
    {
        [Header("Animation Params")]
        [SerializeField] private float _animatingTime;

        [Space, SerializeField] private Ease _animatingEase;

        private Vector3 _defaultScale;

        [FormerlySerializedAs("_text")]
        [Header("View Object")] 
        [SerializeField] private GameObject _viewObject;
        
        private Tween _stateChangingTween;

        private void Awake()
        {
            _defaultScale = _viewObject.transform.localScale;
            _viewObject.transform.localScale = Vector3.zero;
        }

        public void ShowUI()
        {
            KillTweenIfIsNotNullAndActive(_stateChangingTween);

            _stateChangingTween = _viewObject.transform.DOScale(_defaultScale, _animatingTime).SetEase(_animatingEase);
        }

        public void HideUI()
        {
            KillTweenIfIsNotNullAndActive(_stateChangingTween);

            _stateChangingTween = _viewObject.transform.DOScale(Vector3.zero, _animatingTime).SetEase(_animatingEase);
        }

        private void KillTweenIfIsNotNullAndActive(Tween tween)
        {
            if (tween is not null && tween.active)
            {
                tween.Kill();
            }
        }
    }
}