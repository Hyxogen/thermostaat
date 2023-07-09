using System.Collections.Generic;
using UnityEngine;

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

    public void Idle()
    {
        idleTime = Random.Range(10, 25);
    }


    public string GetPreferredWeaponName()
    {
        switch (heroData.heroType)
        {
            case HeroData.Type.BARD:
                return "lute";
            case HeroData.Type.KNIGHT:
                return "sword";
            case HeroData.Type.RANGER:
                return "bow";
            default:
                return null;
        }
    }
}