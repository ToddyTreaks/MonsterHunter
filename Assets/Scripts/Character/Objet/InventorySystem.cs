using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character.Objet
{
    public class InventorySystem : MonoBehaviour
    {
        private List<Item> Inventory = new List<Item>();

        private void AddItem(Item item)
        {
            if (Inventory.Find(x=>x==item))
            {
                item.Add(1);
            }
            Inventory.Add(item);
            Debug.Log(item.name);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var pot = new Potion();
                pot.name = "potion";
                AddItem(pot);
            }
        }
    }
}