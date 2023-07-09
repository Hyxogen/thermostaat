using System.Collections.Generic;

public abstract class Quest : IDialogue
{
    public string questName;
    public string description;

    public Quest(string questName, string description)
    {
        this.questName = questName;
        this.description = description;
    }

    public abstract IEnumerator<IDialogueBase> Next(GameManager manager);
    public abstract IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero);
    public abstract IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero);

    public int Duration()
    {
        return 5;
    }
}