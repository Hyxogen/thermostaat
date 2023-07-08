using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestListUI questList;
    public ItemInventoryUI heroInventory;
    public DialogueManager dialogueManager;
    public List<HeroInstance> allHeroes;
    public List<Quest> allQuests;

    private void Start()
    {
        //heroInventory.SetInventory(hero.inventory);
    }
}
