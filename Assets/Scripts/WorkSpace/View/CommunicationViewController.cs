using DG.Tweening;
using TMPro;
using UnityEngine;

namespace WorkSpace.View
{
    public class CommunicationViewController : MonoBehaviour
    {
        [Header("Animation Params")]
        [SerializeField] private float _animatingTime;

        [Space, SerializeField] private Ease _animatingEase;

        [Space, SerializeField] private Vector3 _defaultScale;

        [Header("Text")] 
        [SerializeField] private TMP_Text _text;
        
        private Tween _stateChangingTween;

        private void Awake()
        {
            _text.transform.localScale = Vector3.zero;
        }

        public void ShowUI()
        {
            KillTweenIfIsNotNullAndActive(_stateChangingTween);

            _stateChangingTween = _text.transform.DOScale(_defaultScale, _animatingTime).SetEase(_animatingEase);
        }

        public void HideUI()
        {
            KillTweenIfIsNotNullAndActive(_stateChangingTween);

            _stateChangingTween = _text.transform.DOScale(Vector3.zero, _animatingTime).SetEase(_animatingEase);
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