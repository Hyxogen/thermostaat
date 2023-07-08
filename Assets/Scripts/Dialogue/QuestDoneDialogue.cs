using System.Collections.Generic;

public class QuestDoneDialogue : IDialogue
{
    private HeroInstance hero;

    public QuestDoneDialogue(HeroInstance hero)
    {
        this.hero = hero;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        return hero.quest.Embark(manager, hero);
    }
}