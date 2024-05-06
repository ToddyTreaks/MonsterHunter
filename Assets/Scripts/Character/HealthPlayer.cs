
using UnityEngine;

public class HealthPlayer : HealthSystem
{

    [SerializeField] PlayerData _playerData;
    [SerializeField] HealthUi _healthUi;
    
    private float maxLife = 0;
    private static bool canHit = true;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        maxLife = _playerData.maxLife;
        SetMaxLife(maxLife);
    }

    private void UpdateUi()
    {
        _healthUi.UpdateFill(GetHealth(), maxLife);
    }
    public override void OnHit()
    {
        if (!canHit){ return; }
        UpdateUi();
    }

    public void ApplyHeal(float heal)
    {
        Heal(heal);
        Debug.Log("heal");
        UpdateUi();
    }

    public static void SetCanHit(bool canhit)
    {
        canHit = canhit;
    }

    public override void OnDeath()
    {
        _animator.CrossFade("Death",0.1f);
    }
}
