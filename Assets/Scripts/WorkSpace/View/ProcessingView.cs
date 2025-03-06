using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WorkSpace.View
{
    public class ProcessingView : MonoBehaviour
    {
        [Header("Animation Params")] 
        [SerializeField] private Ease _processingEase;
        
        [Header("Animation Target")]
        [SerializeField] private Image _progressBar;

        private Tween _processTween;

        public void PlayProcessing(float animationTime)
        {
            KillTweenIfActive(_processTween);

            _progressBar.fillAmount = 0;
            _processTween = _progressBar.DOFillAmount(1, animationTime);
        }

        private void KillTweenIfActive(Tween tween)
        {
            if (tween is not null && tween.active)
            {
                tween.Kill();
            }
        }
    }
}