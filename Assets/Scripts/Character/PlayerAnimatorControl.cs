using UnityEngine;
using static PlayerController;

public class PlayerAnimatorControl : MonoBehaviour
{
    public const float walkSpeed = 0.5f;
    public const float runningSpeed = 1f;

    private Animator _animator;
    private PlayerController _cc;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _cc = GetComponent<PlayerController>();
    }


    internal void UpdateAnimation()
    {
        _animator.SetBool(AnimatorParameters.IsGrounded, _cc._isGrounded);
        _animator.SetFloat(AnimatorParameters.GroundDistance, _cc.distanceToGround);
        _animator.SetBool(AnimatorParameters.IsDashing, _cc.isDashing);
        _animator.SetFloat(AnimatorParameters.Attack, (_cc.typeAttack == Attack.quickAttack) ? 1f : 0f);
        _animator.SetFloat(AnimatorParameters.Velocity,(_cc.stopMove)? 0 : _cc.moveDirection.magnitude, 0.2f,Time.deltaTime);
    }
}

public static partial class AnimatorParameters
{
    public static int Velocity = Animator.StringToHash("velocity");
    public static int IsGrounded = Animator.StringToHash("isGrounded");
    public static int IsStrafing = Animator.StringToHash("isStrafing");
    public static int IsSprinting = Animator.StringToHash("isSprinting");
    public static int IsDashing = Animator.StringToHash("isDashing");
    public static int GroundDistance = Animator.StringToHash("groundDistance");
    public static int Attack = Animator.StringToHash("Attack");
}