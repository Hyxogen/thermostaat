using UnityEngine;
using System.Collections.Generic;

public class QuestListUI : MonoBehaviour
{
    public List<Quest> quests = new();
    public Quest currentQuest;
    public GameObject slotGameObject;

    private QuestUI[] slots;

    private void Start()
    {
        UpdateQuestList();
    }

    private void UpdateQuestList()
    {
        if (slots != null)
        {
            foreach (QuestUI slot in slots)
            {
                Destroy(slot.gameObject);
            }
        }

        slots = new QuestUI[quests.Count];

        for (int i = 0; i < slots.Length; i++)
        {
            GameObject slot = Instantiate(slotGameObject, transform);
            slots[i] = slot.GetComponent<QuestUI>();
            slots[i].questList = this;
            slots[i].SetItem(quests[i]);
        }
    }

    public void AddQuest(Quest quest)
    {
        this.quests.Add(quest);
        UpdateQuestList();
    }

    public void SetCurrentQuest(Quest quest)
    {
        currentQuest = quest;

        foreach (QuestUI slot in slots)
        {
            slot.Select(slot.quest == quest);
        }
    }

    public Quest TakeQuest()
    {
        if (currentQuest != null)
        {
            quests.Remove(currentQuest);
            UpdateQuestList();
        }

        Quest returnQuest = currentQuest;
        currentQuest = null;
        return returnQuest;
    }
}