using Bob.Comunication.Raycating;
using UnityEngine;
using Utiles;
using WorkSpace;

namespace Bob.Comunication
{
    public class BacklightableChecker : MonoBehaviour, IStatable
    {
        private bool _isEnabled = true;

        [SerializeField] private DirectionalRaycaster _forwardRaycaster;

        private IBacklighable _lastBacklightableInViewRange;

        private void OnEnable()
        {
            if (_forwardRaycaster is not null)
            {
                _forwardRaycaster.OnHitObject += HandleObjectHit;
                _forwardRaycaster.OnStopHitting += HandleStopHitting;
            }
        }

        private void OnDisable()
        {
            if (_forwardRaycaster is not null)
            {
                _forwardRaycaster.OnHitObject -= HandleObjectHit;
            }
        }

        private void HandleStopHitting()
        {
            if (_lastBacklightableInViewRange is not null)
            {
                _lastBacklightableInViewRange.DisableBacklight();
                _lastBacklightableInViewRange = null;
            }
        }

        private void HandleObjectHit(RaycastHit hitInfo)
        {
            if (hitInfo.collider.TryGetComponent<IBacklighable>(out var backlightable))
            {
                if (_lastBacklightableInViewRange == backlightable)
                {
                    return;
                }

                _lastBacklightableInViewRange?.DisableBacklight();
                _lastBacklightableInViewRange = backlightable;
                _lastBacklightableInViewRange.EnableBacklight();
            }
            else if (_lastBacklightableInViewRange is not null)
            {
                _lastBacklightableInViewRange.DisableBacklight();
                _lastBacklightableInViewRange = null;
            }
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }
    }
}