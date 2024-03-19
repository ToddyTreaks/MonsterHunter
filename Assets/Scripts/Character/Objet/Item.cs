using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string name;
    public string description;
    public Sprite icon;

    private int _amount;
    private int _amountStockableMax;

    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public int AmountStockableMax
    {
        get { return _amountStockableMax; }
        set { _amountStockableMax = value; }
    }

}
