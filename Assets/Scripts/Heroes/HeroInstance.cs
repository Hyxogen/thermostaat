using System.Collections.Generic;

[System.Serializable]
public class HeroInstance
{
    public HeroData heroData;
    public List<ItemInstance> inventory;
    public Quest quest;
    public int idleTime = 0;
    public int questTime = -1;

    public void StartQuest(Quest quest)
    {
        this.quest = quest;
        this.questTime = quest.Duration();
    }
}