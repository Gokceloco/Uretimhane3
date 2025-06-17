using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerAnimationState playerAnimationState;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void PlayIdleAnimation()
    {
        if (playerAnimationState != PlayerAnimationState.Idle)
        {
            playerAnimationState = PlayerAnimationState.Idle;
            _animator.ResetTrigger("Run");
            _animator.SetTrigger("Idle");
        }
    }
    public void PlayRunAnimation(float angle)
    {
        if (playerAnimationState != PlayerAnimationState.Run)
        {
            playerAnimationState = PlayerAnimationState.Run;
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Run");
        }
        _animator.SetFloat("WalkDirectionAngle", angle);
    }

    public void PlayDrawAnimation()
    {
        _animator.SetTrigger("SwitchWeapon");
    }
}

public enum PlayerAnimationState
{
    Idle,
    Run,
}