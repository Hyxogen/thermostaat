using System.Collections.Generic;

public class CatRescueQuest : Quest
{
    private static string QUEST_NAME = "Cat Rescue";
    private static string DESCRIPTION = "My cat ran away (again...), please bring her back!";
    private static string SPRITE_NAME = "Peasant03";

    private string questGiver;

    public CatRescueQuest(string questGiver) : base(QUEST_NAME, DESCRIPTION)
    {
        this.questGiver = questGiver;
    }

    public override IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool savesCat = false;

        yield return new DialogueText(questGiver, "Hey there John", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Have you seen my cat Whiskers?", SPRITE_NAME);
        yield return new DialogueText(manager.PlayerName(), "Nope", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Darn, was hoping you saw him", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Could you catch him again if he's around?", SPRITE_NAME);

        yield return new LambdaDialogueChoice("Of course!", () => savesCat = true, "No, fuck your cat", () => savesCat = false, SPRITE_NAME);

        if (savesCat)
        {
            manager.questList.AddQuest(this);
            yield return new DialogueText(questGiver, "Thanks man", SPRITE_NAME);
        }
        else
        {
            manager.questQueue.Enqueue(this);
            yield return new DialogueText(questGiver, "Understandable, have a great day", SPRITE_NAME);
        }
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        yield return new DialogueText(manager.PlayerName(), questGiver + " lost their cat, please find it for them.", hero.heroData.spriteName);
        yield return new DialogueText(hero.heroData.heroName, "Sure thing boss.", hero.heroData.spriteName);
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
            yield return new DialogueText(hero.heroData.heroName, "Just so you know, I finished that thing you wanted me to do.", hero.heroData.spriteName);
            yield return new DialogueText("", "You got 50 coins!");
            manager.SetCurrency(manager.currency + 50);
            manager.questQueue.Enqueue(this);
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Fuck fuck fuck fuck fuck fuck fuck.", hero.heroData.spriteName);
            manager.questList.AddQuest(this);
        }

        hero.idleTime = 5;
    }
}