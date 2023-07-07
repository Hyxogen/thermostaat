public abstract class Item
{
    public enum Type
    {
        TUNA,
    }

    public string name { get; private set; }
    public string description { get; private set; }
    public Type type { get; private set; }

    public Item(string name, string description, Type type)
    {
        this.name = name;
        this.description = description;
        this.type = type;
    }
}