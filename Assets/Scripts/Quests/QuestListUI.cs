using UnityEngine;
using System.Collections.Generic;

public class QuestListUI : MonoBehaviour
{
    public List<Quest> quests = new();
    public Quest currentQuest;
    public GameObject slotGameObject;

    private QuestUI[] slots;

    public QuestListUI()
    {
        quests.Add(new CatRescueQuest());
    }

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
                Destroy(slot);
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
}