using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : TooltipProvider, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI text;
    public Image image;
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
            image.enabled = true;
            image.sprite = item.itemData.sprite;
            text.text = item.itemData.itemName;
        }
        else
        {
            text.text = "";
            image.enabled = false;
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

    public override string GetTooltip()
    {
        if (item != null)
        {
            return item.itemData.description;
        }
        else
        {
            return "";
        }
    }
}