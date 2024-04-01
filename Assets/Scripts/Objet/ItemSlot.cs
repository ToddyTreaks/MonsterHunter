using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Assets.Scripts.Character.Objet
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    { 
        private Canvas canvas;

        private void Start()
        {
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                GameObject dropped = eventData.pointerDrag;
                DragAndDrop draggableItem = dropped.GetComponent<DragAndDrop>();
                draggableItem.parentAfterDrag = transform;
                draggableItem.canvas.sortingOrder = 0;
                draggableItem.canvas = canvas;
            }
        }
    }
}