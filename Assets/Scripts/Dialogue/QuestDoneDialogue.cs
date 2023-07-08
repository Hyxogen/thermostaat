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
        if (hero.quest.Embark(hero))
        {
            yield return new DialogueText(hero.heroData.heroName, "Just so you know, I finished that thing you wanted me to do.");
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Fuck fuck fuck fuck fuck fuck fuck.");
        }

        hero.idleTime = 5;
    }
}