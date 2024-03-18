using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieHealthbdf : HealthSystem
{
    private int maxLife = 1000;
    [SerializeField] HealthUi _healthUi;
    void Awake()
    {
        SetMaxLife(maxLife);
    }

    public void ApplyDamage(int damage)
    {
        Damage(damage);
        _healthUi.UpdateFill(GetHealth(), maxLife);
    }
}
