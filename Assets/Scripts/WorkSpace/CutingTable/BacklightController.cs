using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Utiles;

namespace WorkSpace.CutingTable
{
    public class BacklightController : MonoBehaviour
    {
        private bool _isEnabled;

        [SerializeField] private string _backlightStrengthKey;

        [Space, SerializeField] private float _animatingTime;
        private float _currentStrengthValue;
        
        [Space, SerializeField] private Material _highliteMaterial;
        private Material _backlightMaterialInstance;

        [Space, SerializeField] private MeshRenderer _meshRenderer;

        private Tween _effectTween;

        private void Awake()
        {
            _backlightMaterialInstance = new Material(_highliteMaterial);
            
            var materials = _meshRenderer.materials.ToList();
            materials.Add(_backlightMaterialInstance);

            _meshRenderer.materials = materials.ToArray();
        }

        public void EnableBacklight()
        {
            if (_isEnabled)
            {
                return;
            }

            _isEnabled = true;

            DisableEffectTweenIfActive();

            _effectTween = DOTween.To(() => _currentStrengthValue, UpdateStrengthValue, 1, _animatingTime);
        }

        public void DisableBacklight()
        {
            if (!_isEnabled)
            {
                return;
            }

            _isEnabled = false;
            
            DisableEffectTweenIfActive();

            _effectTween = DOTween.To(() => _currentStrengthValue, UpdateStrengthValue, 0, _animatingTime);
        }

        public void DisableEffectTweenIfActive()
        {
            if (_effectTween is not null && _effectTween.active)
            {
                _effectTween.Kill();
                _effectTween = null;
            }
        }

        private void UpdateStrengthValue(float value)
        {
            _currentStrengthValue = value;
            _backlightMaterialInstance.SetFloat(_backlightStrengthKey, value);
        }
    }
}