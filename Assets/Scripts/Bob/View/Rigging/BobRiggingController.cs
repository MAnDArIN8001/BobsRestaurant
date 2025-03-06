using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Bob.View.Rigging
{
    public class BobRiggingController : MonoBehaviour
    {
        [Header("Right Hand")] 
        [SerializeField] private Transform _rightHandIKTarget;
        [SerializeField] private TwoBoneIKConstraint _rightHandConstraint;

        [Space, SerializeField] private ConstraintAnimation _rightHandConstraintAnimation;

        [Space, Header("Left Hand")] 
        [SerializeField] private Transform _leftHandIKTarget;
        [SerializeField] private TwoBoneIKConstraint _leftHandConstraint;

        [Space, SerializeField] private ConstraintAnimation _leftHandConstraintAnimation;

        private void OnDestroy()
        {
            _rightHandConstraintAnimation.Dispose();
            _leftHandConstraintAnimation.Dispose();
        }

        public void SetRightHandRoot(Transform newRoot)
        {
            _rightHandIKTarget.position = newRoot.position;
            _rightHandIKTarget.rotation = newRoot.rotation;
            
            _rightHandConstraintAnimation?.Play(1);
        }

        public void DisableRightHandIK()
        {
            _rightHandConstraintAnimation.Play(0);
        }

        public void SetLeftHandRoot(Transform newRoot)
        {
            _leftHandIKTarget.position = newRoot.position;
            _leftHandIKTarget.rotation = newRoot.rotation;
            
            _leftHandConstraintAnimation.Play(1);
        }

        public void DisableLeftHandIK()
        {
            _leftHandConstraintAnimation.Play(0);
        }
    }
}
