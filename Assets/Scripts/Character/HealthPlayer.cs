
using UnityEngine;

public class HealthPlayer : HealthSystem
{

    [SerializeField] PlayerData _playerData;
    [SerializeField] HealthUi _healthUi;
    private float maxLife = 0;
    void Start()
    {
        maxLife = _playerData.maxLife;
        SetMaxLife(maxLife);
        SetHealth(maxLife);
        
    }

    public void ApplyDamage(float damage)
    {
        Damage(damage);
        _healthUi.UpdateFill(GetHealth(),maxLife);
    }
}
