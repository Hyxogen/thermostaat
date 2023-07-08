using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HeroInstance hero;
    public QuestListUI questList;
    public ItemInventoryUI heroInventory;
    public List<QuestData> allQuests;

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

    public void StartNamedQuest(string name) {
        Debug.Log("Imagine that the quest " + name + " started");
        Debug.Log(hero.heroData.heroName);
        QuestData quest = allQuests.Find(quest => quest.questName == name);
        if (quest == null) {
            Debug.Log("No quest found with the name: " + name);
        } else {
            //questList.quests.Add(quest);
        }
        //questList.quests.Find(quest => quest.questData.questName == name);
    }
}
