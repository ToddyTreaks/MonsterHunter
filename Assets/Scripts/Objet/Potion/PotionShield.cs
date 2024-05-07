using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShield : Item
{
    internal HealthPlayer healthPlayer;
    private int shieldGive = 0;
    private float duration = 0;

    private void Awake()
    {
        shieldGive = _data.ShieldBonus;
        healthPlayer = (HealthPlayer)FindFirstObjectByType(typeof(HealthPlayer));
        if (healthPlayer == null ) Debug.LogError("Not find HealPlayer in PotionShield");
    }

    public override void useItem()
    {
        StartCoroutine(DurationOfItem());
        Remove(1);
    }
    IEnumerator DurationOfItem()
    {
        SetBonusShield(shieldGive);
        yield return new WaitForSeconds(duration);

        SetBonusShield(0);
    }

    private void SetBonusShield(int shieldBonus)
    {
        healthPlayer.shieldPotion = shieldBonus;
    }
}
