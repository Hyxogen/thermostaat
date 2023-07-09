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

        yield return new DialogueText(hero.heroData.heroName, "I'm looking for work.", hero.heroData.spriteName);

        while (quest == null)
        {
            yield return new LambdaDialogueChoice("Start Quest", () => giveQuest = true, "I don't have any", () => giveQuest = false, hero.heroData.spriteName);

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

            foreach (IDialogueBase diag in quest.GiveDialogue(manager, hero))
            {
                yield return diag;
            }

            hero.StartQuest(quest);
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Very well.", hero.heroData.spriteName);
            hero.idleTime = 5;
        }
    }
}