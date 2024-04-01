using Assets.Scripts.Character.Objet;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PrintInventory : MonoBehaviour
    {
        [SerializeField] private AnimationInventory animInventoryPlayer;
        private bool isOpen = false;

        private void Update()
        {
            inputInteract();
        }
        private void inputInteract()
        {
            if (PlayerController.isInteract) Interact();
        }

        private void Interact()
        {
            PlayerController.isInteract = false;
            animInventoryPlayer.TriggerInventoryAnim();
            isOpen = !isOpen;
        }
    }
}