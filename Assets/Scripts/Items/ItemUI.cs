using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI text;
    public ItemInventoryUI inventory;
    public ItemInstance item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            inventory.other.AddItem(item);
            inventory.RemoveItem(item);
        }
    }

    public void SetItem(ItemInstance item)
    {
        this.item = item;

        if (item != null)
        {
            text.text = item.itemData.itemName;
        }
        else
        {
            text.text = "Empty";
        }
    }
}