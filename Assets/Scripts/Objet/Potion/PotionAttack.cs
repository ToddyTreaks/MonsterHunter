using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAttack : Item
{

    private int attackGive = 0;
    private void Awake()
    {
        attackGive = _data.AttackBonus;
    }
}

