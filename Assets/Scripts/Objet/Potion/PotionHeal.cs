using System.Collections;
using Assets.Scripts.Objet.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PotionHeal: Item
{

    private int healtGive = 0;
    private HealthPlayer healthPlayer;
    private void Awake()
    {
        healtGive = _data.Heal;
        healthPlayer = (HealthPlayer)FindFirstObjectByType(typeof(HealthPlayer));
    }

    public override void useItem()
    {
        healthPlayer.ApplyHeal(healtGive);
        if (healthPlayer == null ) { Debug.Log("not find HealthPlayer");}
        Remove(1);
    }
}