public abstract class Quest
{
    public string name { get; private set; }
    public string description { get; private set; }

    public Quest(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public abstract bool Embark(Hero hero);
}
