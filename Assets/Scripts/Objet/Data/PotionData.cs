using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objet.Data;
using UnityEngine;

[CreateAssetMenu]
public class PotionData : ItemData
{
    [Range(1,100)]
    [Tooltip("nbMaxPotion;")]
    public int nbMaxPotion = 0;

    [Range(1, 100)]
    [Tooltip("Heal")]
    public int Heal = 0;

    [Range(1, 100)]
    [Tooltip("AttackBonus")]
    public int AttackBonus = 0;

    [Range(1, 100)]
    [Tooltip("ShieldBonus")]
    public int ShieldBonus = 0;

    [Range(0, 10)] 
    [Tooltip(("Duration"))] 
    public float duration = 1;
}
