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
        _animator.SetLayerWeight(1, 1);
        Invoke(nameof(SetLayer1MaskToZero), 1f);
    }    

    public void PlayThrowGrenadeAnimation()
    {
        _animator.SetTrigger("ThrowGrenade");
        _animator.SetLayerWeight(1, 1);
        Invoke(nameof(SetLayer1MaskToZero), 1f);
    }
    void SetLayer1MaskToZero()
    {
        _animator.SetLayerWeight(1, 0);
    }

    public void PlayFallBackAnimation()
    {
        playerAnimationState = PlayerAnimationState.Dead;
        _animator.SetTrigger("FallBack");
    }

    public void PlayFallDownAnimation()
    {
        playerAnimationState = PlayerAnimationState.Dead;
        _animator.SetTrigger("FallDown");
    }
}

public enum PlayerAnimationState
{
    Idle,
    Run,
    Dead,
}