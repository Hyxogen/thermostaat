[System.Serializable]
public class QuestInstance
{
    public QuestData questData;

    // TODO: this whole function is trash
    // - items should not be identified by their display name
    // - should be customizable for different types of quests
    // - not just return a boolean? maybe return some QuestOutcome that also stores how long it took and stuff
    // - consume items?
    public bool Embark(HeroInstance hero)
    {
        foreach (ItemInstance item in hero.inventory.items)
        {
            if (item.itemData.itemName == "Tuna")
            {
                return true;
            }
        }

        return false;
    }
}