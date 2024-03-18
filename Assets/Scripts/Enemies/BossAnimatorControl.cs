using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorControl : MonoBehaviour
{
    private Animator _animator;
    private Boss_Navigation _navigation;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _navigation = GetComponent<Boss_Navigation>();
    }


    internal void UpdateAnimation()
    {
        _animator.SetFloat(AnimatorParameters.speed, _navigation.Speed);
        _animator.SetFloat(AnimatorParameters.attack, _navigation.Attack);
        _animator.SetFloat(AnimatorParameters.invocation, _navigation.Invocation);
        _animator.SetFloat(AnimatorParameters.shoot, _navigation.Shoot);
        
        _animator.SetBool(AnimatorParameters.isAttacking, _navigation.IsAttacking);
        _animator.SetBool(AnimatorParameters.isFighting, _navigation.IsFighting);
        _animator.SetBool(AnimatorParameters.isDead, _navigation.IsDead);
    }
}

public static partial class AnimatorParameters
{
    public static int speed = Animator.StringToHash("Speed");
    public static int attack = Animator.StringToHash("Attack");
    public static int invocation = Animator.StringToHash("Invocation");
    public static int shoot = Animator.StringToHash("Shoot");
    public static int isAttacking = Animator.StringToHash("isAttacking");
    public static int isFighting = Animator.StringToHash("isFighting");
    public static int isDead = Animator.StringToHash("isDead");
}