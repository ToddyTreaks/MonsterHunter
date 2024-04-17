using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    protected float _maxLife = 0;
    protected float _health = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Damage(float health)
    {
        
        _health -= health;
        if (_health <= 0)
        {
            animator.SetTrigger("Death");
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public void Heal(float health)
    {
        _health += health;
        if (_health > _maxLife)
        {
            _health = _maxLife;
        }
    }

    public float GetHealth()
    { return _health; }

    public void SetMaxLife(float maxLife)
    {
        _maxLife = maxLife;
    }
}
