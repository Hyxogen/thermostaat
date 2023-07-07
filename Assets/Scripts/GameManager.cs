using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HeroInstance hero;
    public QuestListUI questList;
    public ItemInventoryUI heroInventory;

    private void Start()
    {
        heroInventory.SetInventory(hero.inventory);
    }

    public void StartQuest()
    {
        if (questList.currentQuest.Embark(hero))
        {
            Debug.Log("POGGERS");
        }
        else
        {
            Debug.LogError("NOT POGGERS");
        }
    }
}
