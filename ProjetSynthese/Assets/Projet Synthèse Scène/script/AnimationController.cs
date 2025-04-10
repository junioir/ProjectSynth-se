using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void ResetAttack()
    {
        _animator.SetBool("IsAttacking", false);
    }

    public void SetIsAttacking()
    {
        _animator.SetBool("IsAttacking", true);
    }

    public void SetIsWalking()
    {
        _animator.SetBool("IsWalking", true);
    }

    public void SetIsNotWalking()
    {
        _animator.SetBool("IsWalking", false);
    }
    public void SetIsDying()
    {
        _animator.SetBool("die", true);
    }
}
