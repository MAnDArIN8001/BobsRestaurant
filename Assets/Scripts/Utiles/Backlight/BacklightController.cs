using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Utiles.Backlight
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
        
        private List<Material> _materials;

        private Tween _effectTween;

        private void Awake()
        {
            _backlightMaterialInstance = new Material(_highliteMaterial);
            _materials = _meshRenderer.materials.ToList();
        }

        public void EnableBacklight()
        {
            if (_isEnabled)
            {
                return;
            }

            _isEnabled = true;

            DisableEffectTweenIfActive();

            _materials.Add(_backlightMaterialInstance);
            _meshRenderer.SetMaterials(_materials);

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

            _materials.Remove(_backlightMaterialInstance);

            _effectTween = DOTween.To(() => _currentStrengthValue, UpdateStrengthValue, 0, _animatingTime).OnComplete(() => _meshRenderer.SetMaterials(_materials));
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