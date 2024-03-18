using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : HealthSystem
{

    [SerializeField] PlayerData _playerData;
    [SerializeField] HealthUi _healthUi;
    private float maxLife;
    void Awake()
    {
        maxLife = _playerData.maxLife;

    SetMaxLife(maxLife);
    }

    public void ApplyDamage(float damage)
    {
        Damage(damage);
        _healthUi.UpdateFill(GetHealth(), maxLife);
    }
}
