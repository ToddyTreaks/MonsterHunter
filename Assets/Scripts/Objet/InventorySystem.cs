using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Character.Objet
{
    public class InventorySystem : MonoBehaviour
    {
        public string name;
        private List<Item> Inventory = new List<Item>();

        private void AddItem(Item item)
        {
            if (Inventory.Find(x=>x==item))
            {
                Debug.Log("in addItem : "+item.Name + " nb = " + item.getAmount());
                item.Add(item.getAmount());
            }
            Inventory.Add(item);
        }

        private void RemoveItem(Item item)
        {
            Inventory.Remove(item);
        }

        public void AddInInventory(Item item)
        {
            if (Inventory.Find(x => item.Equals(x)))
            {
                return;
            }
            AddItem(item);
            PrintInventory();
        }

        public void RemoveInInventory(Item item)
        {
            RemoveItem(item);
            PrintInventory();
        }


        private void PrintInventory()
        {
            Debug.Log(name + " ");
            foreach(Item item in Inventory)
            {
                Debug.Log(item.Name + " nb = " + item.getAmount());
            }
        }
    }
}