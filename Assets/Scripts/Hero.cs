public class Hero
{
    public string name { get; private set; }

    public Item[] inventory { get; private set; }

    public Hero(string name)
    {
        this.name = name;
    }
}