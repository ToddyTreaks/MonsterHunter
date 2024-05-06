using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAttack : Item
{
    private int attackGive = 0;
    private float duration = 0;
    private void Awake()
    {
        attackGive = _data.AttackBonus;
        duration = _data.duration;
    }

    public override void useItem()
    {
        DurationOfItem();
    }

    IEnumerator DurationOfItem()
    {
        SetBonusAttack(attackGive);
        yield return new WaitForSeconds(duration);

        SetBonusAttack(0);
        yield return null;
    }

    private void SetBonusAttack(int attackBonus)
    {
        AttackScript.BonusDamage = attackBonus;
        Debug.Log("f");
    }
}

