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
        foreach (IDialogueBase diag in hero.quest.Embark(manager, hero))
        {
            yield return diag;
        }

        if (hero.inventory.Count > 0)
        {
            foreach (ItemInstance item in hero.inventory)
            {
                manager.itemInventory.AddItem(item);
            }

            hero.inventory.Clear();

            yield return new DialogueText("", "You got some of your items back!");
        }
    }
}