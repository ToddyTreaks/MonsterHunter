using Assets.Scripts.Objet.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public string description;
    protected Sprite icon;

    protected int _amount;
    protected int _amountStockableMax;

    private static int seed = 0;
    protected int IdItem = ++seed;

    private NumberItem _numberPrint;
    [SerializeField] protected PotionData _data;

    private void Start()
    {
        _numberPrint = GetComponentInChildren<NumberItem>();
        _amountStockableMax = _data.nbMaxPotion;
        _amount = _amountStockableMax;
        Name = _data.name;
        description = _data.description;
        icon = _data.icon;

        printAmount();
    }
    public void Add(int nb)
    {
        _amount += nb;
        if (_amount > _amountStockableMax) 
        { _amount = _amountStockableMax; }

        printAmount();
    }

    public void Remove(int nb)
    {
        _amount -= nb;
        if (_amount<=0) { _amount = 0; }
        printAmount();
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Item objAsItem = obj as Item;
        if (objAsItem == null) return false;
        else return Equals(objAsItem);
    }
    public override int GetHashCode()
    {
        return IdItem;
    }
    public bool Equals(Item other)
    {
        if (other == null) return false;
        return (this.IdItem.Equals(other.IdItem));
    }

    public int getAmount()
    {
        return _amount;
    }

    public void printAmount()
    {
        _numberPrint.PrintAmount(getAmount());
    }
}
