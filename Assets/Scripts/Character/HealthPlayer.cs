
using UnityEngine;

public class HealthPlayer : HealthSystem
{

    [SerializeField] PlayerData _playerData;
    [SerializeField] HealthUi _healthUi;

    private float maxLife = 0;
    private bool isInvinsible = false;
    void Start()
    {
        maxLife = _playerData.maxLife;
        SetMaxLife(maxLife);
    }
    private void UpdateUi()
    {
        _healthUi.UpdateFill(GetHealth(), maxLife);
    }
    public override void OnHit()
    {
        if (isInvinsible){ return; }
        UpdateUi();
    }

    public void ApplyHeal(float heal)
    {
        Heal(heal);
        UpdateUi();
    }

    public void IsInvinsible(bool _isInvinsible)
    {
        isInvinsible = _isInvinsible;
    }

    public override void OnDeath()
    {

    }
}
