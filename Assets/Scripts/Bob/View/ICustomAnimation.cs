using System;
using DG.Tweening;

namespace Bob.View
{
    public interface ICustomAnimation<T>
    {
        public void Play(T endValue, TweenCallback callBack = null);
        public void Stop();
    }
}