using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string name;
    public string description;
    public Sprite icon;

    protected int _amount;
    protected int _amountStockableMax;

    public void Add(int nb)
    {
        _amount += nb;
        if (_amount > _amountStockableMax) 
        { _amount = _amountStockableMax; }
    }

    public void Remove(int nb)
    {
        _amount -= nb;
        if (_amount<=0) { _amount = 0; }
    }
}
