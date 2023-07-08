public abstract class Quest
{
    public string questName;
    public string description;

    public Quest(string questName, string description)
    {
        this.questName = questName;
        this.description = description;
    }

    public abstract bool Embark(HeroInstance hero);

    public int Duration()
    {
        return 5;
    }
}