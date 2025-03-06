using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Bob.View
{
    [Serializable]
    public class ConstraintAnimation : IDisposable, ICustomAnimation<float>
    {
        private float _currentWeight;
        [SerializeField] private float _animatingTime;

        [Space, SerializeField] private Ease _animatingEase;

        [Space, SerializeField] private TwoBoneIKConstraint _targetConstraint;
        
        private Tween _animationTween;

        private void UpdateWeight(float weight)
        {
            if (_targetConstraint is not null)
            {
                _currentWeight = weight;
                _targetConstraint.weight = weight;
            }
        }

        public void Play(float endValue, TweenCallback callBack = null)
        {
            Stop();

            _animationTween = DOTween.To(() => _currentWeight, UpdateWeight, endValue, _animatingTime).SetEase(_animatingEase);

            if (callBack is not null)
            {
                _animationTween.OnComplete(callBack);
            }
        }
        
        public void Stop()
        {
            if (_animationTween is not null && _animationTween.active)
            {
                _animationTween.Kill();
                
                _animationTween = null;
            } 
        }

        public void Dispose()
        {
            Stop();
        }
    }
}