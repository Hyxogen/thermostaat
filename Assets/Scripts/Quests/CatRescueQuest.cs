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

        yield return new DialogueText(questGiver, "Hey there John");
        yield return new DialogueText(questGiver, "Have you seen my cat Whiskers?");
        yield return new DialogueText(manager.PlayerName(), "Nope");
        yield return new DialogueText(questGiver, "Darn, was hoping you saw him");
        yield return new DialogueText(questGiver, "Could you catch him again if he's around?");

        yield return new LambdaDialogueChoice("Yeah ofcource", () => savesCat = true, "No, fuck your cat", () => savesCat = false);

        if (savesCat)
        {
            manager.questList.AddQuest(this);
            yield return new DialogueText(questGiver, "Thanks man");
        }
        else
        {
            manager.questQueue.Enqueue(this);
            yield return new DialogueText(questGiver, "Understandable, have a great day");
        }
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        yield return new DialogueText(manager.PlayerName(), questGiver + " lost their cat, please find it for them.");
        yield return new DialogueText(hero.heroData.heroName, "Sure thing boss.");
    }

    public override bool Test(HeroInstance hero)
    {
        foreach (ItemInstance item in hero.inventory)
        {
            if (item.itemData.itemType == ItemData.Type.TUNA)
            {
                return true;
            }
        }

        return false;
    }

    public override IEnumerator<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        if (Test(hero))
        {
            yield return new DialogueText(hero.heroData.heroName, "Just so you know, I finished that thing you wanted me to do.");
            manager.questQueue.Enqueue(this);
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Fuck fuck fuck fuck fuck fuck fuck.");
            manager.questList.AddQuest(this);
        }

        hero.idleTime = 5;
    }
}