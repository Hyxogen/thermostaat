using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestListUI questList;
    public ItemInventoryUI itemInventory;
    public DialogueManager dialogueManager;
    public List<HeroInstance> allHeroes;
    public Queue<Quest> questQueue = new();
    public List<ItemInstance> shopItems;
    public int currency = 100;

    public GameManager()
    {
        questQueue.Enqueue(new CatRescueQuest("David"));
    }

    public string PlayerName()
    {
        return "You";
    }
}
