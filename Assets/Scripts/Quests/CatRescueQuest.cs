public class CatRescueQuest : Quest
{
    private static string QUEST_NAME = "Cat Rescue";
    private static string DESCRIPTION = "My cat ran away (again...), please bring her back!";

    public CatRescueQuest() : base(QUEST_NAME, DESCRIPTION)
    {
    }

    public override bool Embark(HeroInstance hero)
    {
        foreach (ItemInstance item in hero.inventory)
        {
            if (item.itemData.itemType == ItemData.Type.TUNA)
            {
                return true;
            }
        }

        return false;
    }
}