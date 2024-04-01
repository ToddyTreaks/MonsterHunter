
using Assets.Scripts.Character.Objet;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour,
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [HideInInspector] public Canvas canvas;

    [HideInInspector] public Transform parentAfterDrag;
    private InventorySystem inventorySystem;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        rectTransform.SetAsLastSibling();
        canvas.sortingOrder = 1;

        Debug.Log("take item");
        GameObject dropped = eventData.pointerDrag;
        inventorySystem = canvas.GetComponentInChildren<InventorySystem>();//remove in inventory 
        if (inventorySystem == null) Debug.Log("not found inventorySystem");
        if (dropped.TryGetComponent<Item>(out var item))
        {
            inventorySystem.RemoveInInventory(item);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentAfterDrag);
        rectTransform.anchoredPosition = Vector3.zero;
        canvas.sortingOrder = 0;

        GameObject dropped = eventData.pointerDrag;
        Debug.Log("drop item");
        inventorySystem = canvas.GetComponentInChildren<InventorySystem>();// add in inventory 
        if (inventorySystem == null) Debug.Log("not found inventorySystem");
        if (dropped.transform.TryGetComponent<Item>(out var item))
        {
            inventorySystem.AddInInventory(item);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }
}
