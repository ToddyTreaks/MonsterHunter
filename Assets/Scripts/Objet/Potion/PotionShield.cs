using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShield : Item
{
    internal HealthPlayer healthPlayer;
    private int shieldGive = 0;
    private float duration = 0;
    private void Start()
    {
        shieldGive = _data.Heal;
        healthPlayer = (HealthPlayer)FindFirstObjectByType(typeof(HealthPlayer));
    }

    public override void useItem()
    {
        DurationOfItem();
    }
    IEnumerator DurationOfItem()
    {
        SetBonusAttack(shieldGive);
        yield return new WaitForSeconds(duration);

        SetBonusAttack(0);
        yield return null;
    }

    private void SetBonusAttack(int shieldBonus)
    {
        healthPlayer.shieldPotion = shieldBonus;
    }
}
