using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float _maxLife;
    private float _health;

    void Start()
    {
        _health = _maxLife;
    }

    public void Damage(float health)
    {
        _health -= health;
        if (_health <= 0)
        {
            Destroy(gameObject);
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

    public void SetMaxLife(int maxLife)
    {
        _maxLife = maxLife;
    }
}
