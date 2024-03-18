using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _maxLife;
    private int _health;

    void Start()
    {
        _health = _maxLife;
    }

    public void Damage(int health)
    {
        _health -= health;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int health)
    {
        _health += health;
        if (_health < _maxLife)
        {
            _health = _maxLife;
        }
    }

    public int GetHealth()
    { return _health; }

    public void SetMaxLife(int maxLife)
    {
        _maxLife = maxLife;
    }
}
