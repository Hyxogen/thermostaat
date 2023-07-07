public class CatRescueQuest : Quest
{
    private static string NAME = "Cat Rescue";
    private static string DESCRIPTION = "My cat ran away (again...), please get her back!";

    public CatRescueQuest() : base(NAME, DESCRIPTION)
    {
    }

    public override bool Embark(Hero hero)
    {
        foreach (Item item in hero.inventory)
        {
            if (item.type == Item.Type.TUNA)
            {
                return true;
            }
        }

        return false;
    }
}