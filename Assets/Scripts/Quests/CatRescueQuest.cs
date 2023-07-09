using System.Collections.Generic;

public class CatRescueQuest : Quest
{
    private static string QUEST_NAME = "Cat Rescue";
    private static string DESCRIPTION = "My cat ran away (again...), please bring her back!";

    private string questGiver;

    public CatRescueQuest(string questGiver) : base(QUEST_NAME, DESCRIPTION)
    {
        this.questGiver = questGiver;
    }

    public override IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool savesCat = false;

        yield return new DialogueText(questGiver, "Hey there " + manager.PlayerName() + ".");
        yield return new DialogueText(questGiver, "Have you seen my cat Whiskers?");
        yield return new DialogueText(manager.PlayerIdentifier(), "Nope");
        yield return new DialogueText(questGiver, "Darn, was hoping you saw him.");
        yield return new DialogueText(questGiver, "Could you catch him again if he's around?");

        yield return new LambdaDialogueChoice("Of course!", () => savesCat = true, "No!", () => savesCat = false);

        if (savesCat)
        {
            manager.questList.AddQuest(this);
            yield return new DialogueText(questGiver, "Thanks man.");
        }
        else
        {
            manager.questQueue.Enqueue(this);
            yield return new DialogueText(questGiver, "Understandable, have a great day.");
        }
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        yield return new DialogueText(manager.PlayerIdentifier(), questGiver + " lost their cat, please find it for them.");
        yield return new DialogueText(hero.heroData.heroName, "Sure thing boss.");
    }

    public override bool Test(HeroInstance hero)
    {
        ItemInstance tuna = null;

        foreach (ItemInstance item in hero.inventory)
        {
            if (item.itemData.itemType == ItemData.Type.TUNA)
            {
                tuna = item;
                break;
            }
        }

        if (tuna != null)
        {
            hero.inventory.Remove(tuna);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        if (Test(hero))
        {
            yield return new DialogueText(hero.heroData.heroName, "Just so you know, I found the cat.");
            yield return new DialogueText("", "You got 50 coins!");
            manager.SetCurrency(manager.currency + 50);
            manager.questQueue.Enqueue(this);
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "I was not able to find that cat you told me about.");
            yield return new DialogueText(hero.heroData.heroName, "You might have better luck if you had something to lure it with.");
            manager.questList.AddQuest(this);
        }

        hero.idleTime = 5;
    }
}