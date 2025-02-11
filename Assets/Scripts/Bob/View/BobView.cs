using System;
using Bob.Controls;
using UnityEngine;

public class BobView : MonoBehaviour
{
    [Header("Animator keys")] 
    [SerializeField] private string _horizontalMovementKey;
    [SerializeField] private string _verticalMovementKey;
    [SerializeField] private string _jumpKey;

    private Animator _animator;

    #region BobComponents

    private BobMovement _movement;
    private BobJump _jumper;
    
    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<BobMovement>();
        _jumper = GetComponent<BobJump>();
    }

    private void OnEnable()
    {
        if (_movement is not null)
        {
            _movement.OnMovementCompute += HandleMovementCompute;
        }

        if (_jumper is not null)
        {
            _jumper.OnGroundedStateChanged += HandleGroundedStateChanged;
        }
    }
    
    private void OnDisable()
    {
        if (_movement is not null)
        {
            _movement.OnMovementCompute -= HandleMovementCompute;
        }
        
        if (_jumper is not null)
        {
            _jumper.OnGroundedStateChanged -= HandleGroundedStateChanged;
        }
    }

    private void HandleMovementCompute(Vector2 obj)
    {
        _animator.SetFloat(_horizontalMovementKey, obj.x);
        _animator.SetFloat(_verticalMovementKey, obj.y);
    }

    private void HandleGroundedStateChanged(bool newGroundedState)
    {
        _animator.SetBool(_jumpKey, newGroundedState);
    }
}
