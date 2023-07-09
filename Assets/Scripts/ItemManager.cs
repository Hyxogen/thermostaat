using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;

    public ItemInstance ladder;
    public ItemInstance acid;
    public ItemInstance scroll;

    public static ItemManager Instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ItemManager>();
        }

        return instance;
    }
}
