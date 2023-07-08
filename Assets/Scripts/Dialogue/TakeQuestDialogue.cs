using System.Collections.Generic;

public class TakeQuestDialogue : IDialogue
{
    private HeroInstance hero;

    public TakeQuestDialogue(HeroInstance hero)
    {
        this.hero = hero;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool giveQuest = false;
        Quest quest = null;

        yield return new DialogueText(hero.heroData.heroName, "I'm looking for work.");

        while (quest == null)
        {
            yield return new LambdaDialogueChoice("Start Quest", () => giveQuest = true, "I don't have any", () => giveQuest = false);

            if (giveQuest)
            {
                quest = manager.questList.TakeQuest();
            }
            else
            {
                break;
            }
        }

        if (quest != null)
        {
            foreach (ItemInstance item in manager.itemInventory.TakeItems())
            {
                hero.inventory.Add(item);
            }

            yield return new DialogueText(hero.heroData.heroName, "You got it boss.");
            hero.StartQuest(quest);
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Very well.");
            hero.idleTime = 5;
        }
    }
}