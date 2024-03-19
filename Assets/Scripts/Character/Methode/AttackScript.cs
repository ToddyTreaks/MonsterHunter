using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private Animator _animator;

    internal enum Attack
    {
        quickAttack = 0, longAttack = 1,
    }
    internal Attack typeAttack;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        InputAttack();
    }

    private void InputAttack()
    {
        if (_animator.GetCurrentAnimatorStateInfo(1).IsName("Attack")) return;
        if (Input.GetButtonDown("quickAttack"))
        {
            QuickAttack();
            return;
        }

        if (Input.GetButtonDown("longAttack"))
        {
            LongAttack();
            return;
        }
    }

    #region Attack

    public void QuickAttack()
    {
        typeAttack = Attack.quickAttack;
        _animator.CrossFade("Attack", 0.1f);
    }

    public void LongAttack()
    {
        typeAttack = Attack.longAttack;
        _animator.CrossFade("Attack", 0.1f);
    }
    #endregion
}