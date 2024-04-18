using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Character.Methode
{
    public class ItemUsed : MonoBehaviour
    {
        [SerializeField] private List<GameObject> listItemSlot;

        private void Update()
        {
            inputItem();
        }
        private void inputItem()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseItem(listItemSlot[0]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseItem(listItemSlot[1]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseItem(listItemSlot[2]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseItem(listItemSlot[3]);
            }
        }

        private void UseItem(GameObject itemBackground)
        {
            Item item = itemBackground.transform.GetComponentInChildren<Item>();

            if (item!=null)
            {
                item.useItem();
            }
        }
    }
}