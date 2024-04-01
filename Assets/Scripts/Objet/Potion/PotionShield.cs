using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShield : Item
{

    private int shieldGive = 0;
    private void Awake()
    {
        shieldGive = _data.Heal;
    }
}
