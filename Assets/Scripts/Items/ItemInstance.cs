[System.Serializable]
public class ItemInstance
{
    public ItemData itemData;

    public ItemInstance(ItemInstance other)
    {
        itemData = other.itemData;
    }
}