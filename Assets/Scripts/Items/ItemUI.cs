using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI text;
    public ItemInstance item;
    public bool selected = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            Select(!selected);
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

    public void Select(bool selected)
    {
        this.selected = selected;

        if (selected)
        {
            text.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else
        {
            text.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
}