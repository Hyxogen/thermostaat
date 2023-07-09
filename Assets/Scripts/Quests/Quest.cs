using System.Collections.Generic;
using System.Linq;

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

    public virtual IEnumerable<IDialogueBase> QuestCompleted(GameManager manager)
    {
        return Enumerable.Empty<IDialogueBase>();
    }

    public virtual int Duration()
    {
        return 5;
    }
}