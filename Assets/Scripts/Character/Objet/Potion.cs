using System.Collections;
using UnityEngine;

public class Potion : Item
{
    [SerializeField] private ItemData _data;

    public int healtGive = 0;

    void Start()
    {
        healtGive = _data.HealPotion;
        /*AmountStockableMax*/
    }
}