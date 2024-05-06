using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should be inherited
public class HealthSystem : MonoBehaviour
{
    private float _maxLife = 100f;
    private float _health = 100f;

    internal float shieldPotion = 0;

    public void Damage(float health)
    {
        _health -= health - shieldPotion;

        if (_health <= 0)
        {
            OnDeath();
        }
        else
        {
            OnHit();
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

    protected float GetHealth()
    { return _health; }

    public void SetMaxLife(float maxLife)
    {
        _maxLife = maxLife;
        _health = maxLife;
    }
    public virtual void OnDeath(){}

    public virtual void OnHit(){}
}
