using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthSystem : EnemyHealthSystem
{
    [SerializeField] private Slider _slider;

    public override void OnHit()
    {
        base.OnHit();
        _slider.value = GetHealth() / _maxLife;
        Debug.Log("Boss Hit");
    }
}