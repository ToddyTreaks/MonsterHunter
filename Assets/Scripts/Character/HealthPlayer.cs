
using UnityEngine;

public class HealthPlayer : HealthSystem
{

    [SerializeField] PlayerData _playerData;
    [SerializeField] HealthUi _healthUi;
    void Start()
    {
        _maxLife = _playerData.maxLife;
        _health = _maxLife;
        SetMaxLife(_maxLife);
    }

    public void ApplyDamage(float damage)
    {
        Damage(damage);
        _healthUi.UpdateFill(GetHealth(),_maxLife);
    }
}
