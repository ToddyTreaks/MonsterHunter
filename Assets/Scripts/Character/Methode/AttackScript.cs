using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private int layer = 1;

    private Animator _animator;
    private float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int nbOfCliks = 0;
    private float lastClickedTime = 0f;
    private float maxComboDelay = 2f;

    public static bool StopMoveWhenAttack = false;
    public static int BonusDamage = 0;

    [SerializeField] private AttackData _attackData;
    internal enum Attack
    {
        quickAttack = 0, longAttack = 1,
    }

    internal Attack typeAttack;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        if (PlayerController.StopPlayer) return;
        ResetBoolAnim();

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            nbOfCliks = 0;
        }

        if (Time.time > nextFireTime)
        {
            InputAttack();
        }

        StopPlayerMovement();
    }

    void StopPlayerMovement()
    {
        StopMoveWhenAttack = !_animator.GetCurrentAnimatorStateInfo(1).IsName("empty");
        /*StopMoveWhenAttack = !_animator.GetCurrentAnimatorStateInfo(layer).IsName("Blend Tree");*/
    }
    private void InputAttack()
    {
        if (Input.GetButtonDown("quickAttack"))
        {
            QuickAttack();
            OnClick();
            return;
        }
        if (Input.GetButtonDown("longAttack"))
        {
            LongAttack();
            OnClick();
            return;
        }
    }

    private void OnClick()
    {
        lastClickedTime = Time.time;
        nbOfCliks++;
        if (nbOfCliks == 1)
        {
            _animator.SetBool("hit1", true);
        }

        nbOfCliks = Mathf.Clamp(nbOfCliks, 0, 3);

        if (nbOfCliks >= 2 
            && _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime > 0.5f
            && _animator.GetCurrentAnimatorStateInfo(layer).IsName("Attack1"))
        {
            _animator.SetBool("hit1", false);
            _animator.SetBool("hit2", true);
        }
        if (nbOfCliks >= 3
            && _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime > 0.5f
            && _animator.GetCurrentAnimatorStateInfo(layer).IsName("Attack2"))
        {
            _animator.SetBool("hit2", false);
            _animator.SetBool("hit3",true);
        }
    }

    private void ResetBoolAnim()
    {
        if (_animator.GetCurrentAnimatorStateInfo(layer).normalizedTime > 0.7f
            && _animator.GetCurrentAnimatorStateInfo(layer).IsName("Attack1"))
        {
            _animator.SetBool("hit1", false);
            _animator.SetBool("Combo1", false);
            _animator.SetBool("Combo2", false);
        }
        if (_animator.GetCurrentAnimatorStateInfo(layer).normalizedTime > 0.7f
            && _animator.GetCurrentAnimatorStateInfo(layer).IsName("Attack2"))
        {
            _animator.SetBool("hit2", false);
        }
        if (_animator.GetCurrentAnimatorStateInfo(layer).normalizedTime > 0.7f
            && _animator.GetCurrentAnimatorStateInfo(layer).IsName("Attack3"))
        {
            _animator.SetBool("hit3", false);
            nbOfCliks = 0;
        }

    }
    #region Attack

    public void QuickAttack()
    {
        typeAttack = Attack.quickAttack;
        _animator.SetBool("Combo1",true);
        SwordDetection.Damage = _attackData.DamageQuickAttaque + BonusDamage;
    }

    public void LongAttack()
    {
        typeAttack = Attack.longAttack;
        _animator.SetBool("Combo2",true);
        SwordDetection.Damage = _attackData.DamageLongAttaque + BonusDamage;
    }
    #endregion
}