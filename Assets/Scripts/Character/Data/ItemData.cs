using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [Range(10, 1000)]
    [Tooltip("Heal Potion")]
    public int HealPotion = 100;

    [Range(1, 10)]
    [Tooltip("Amount Max Potion")]
    public int nbMaxPotion = 1;
}
