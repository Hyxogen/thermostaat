using UnityEngine;

public class ItemInventoryUI : MonoBehaviour
{
    public ItemInventory inventory;
    public int slotsCount;
    public GameObject slotGameObject;
    public ItemInventoryUI other;

    private ItemUI[] slots;

    private void Start()
    {
        slots = new ItemUI[slotsCount];

        for (int i = 0; i < slotsCount; i++)
        {
            Vector3 position = new Vector3(0.0f, i * -1.0f, 0.0f);
            GameObject slot = Instantiate(slotGameObject, position, Quaternion.identity, transform);
            slots[i] = slot.GetComponent<ItemUI>();
            slots[i].inventory = this;
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
            if (i < inventory.items.Count)
            {
                slots[i].SetItem(inventory.items[i]);
            }
            else
            {
                slots[i].SetItem(null);
            }
        }
    }

    public void SetInventory(ItemInventory inventory)
    {
        this.inventory = inventory;
        UpdateInventory();
    }

    public void AddItem(ItemInstance item)
    {
        inventory.items.Add(item);
        UpdateInventory();
    }

    public void RemoveItem(ItemInstance item)
    {
        inventory.items.Remove(item);
        UpdateInventory();
    }
}