using UnityEngine;
using System.Collections.Generic;

public class ItemInventoryUI : MonoBehaviour
{
    public List<ItemInstance> items = new();
    public int slotsCount;
    public GameObject slotGameObject;

    private ItemUI[] slots;

    private void Start()
    {
        slots = new ItemUI[slotsCount];

        for (int i = 0; i < slotsCount; i++)
        {
            Vector3 position = new Vector3(0.0f, i * -1.0f, 0.0f);
            GameObject slot = Instantiate(slotGameObject, position, Quaternion.identity, transform);
            slots[i] = slot.GetComponent<ItemUI>();
        }

        UpdateInventory();
    }

    private void UpdateInventory()
    {
        if (slots == null)
        {
            return;
        }
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].SetItem(null);
            }
        }
    }

    public void AddItem(ItemInstance item)
    {
        items.Add(item);
        UpdateInventory();
    }

    public void RemoveItem(ItemInstance item)
    {
        items.Remove(item);
        UpdateInventory();
    }

    public List<ItemInstance> TakeItems()
    {
        List<ItemInstance> returnItems = new();

        foreach (ItemUI item in slots)
        {
            if (item.selected)
            {
                item.Select(false);
                items.Remove(item.item);
                returnItems.Add(item.item);
            }
        }

        UpdateInventory();
        return returnItems;
    }
}