using System.Collections;
using Assets.Scripts.Objet.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PotionHeal: Item
{

    private int healtGive = 0;
    private void Awake()
    {
        healtGive = _data.Heal;
    }
}