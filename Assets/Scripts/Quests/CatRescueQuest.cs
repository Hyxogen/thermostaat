using System.Collections.Generic;
using UnityEngine;

public class CatRescueQuest : Quest
{
    public enum Variant
    {
        NORMAL,
        LADDER,
    }

    private static string QUEST_NAME = "Cat Rescue";
    private static string DESCRIPTION = "My cat ran away (again...), please bring her back!";
    private static string SPRITE_NAME = "Peasant03";

    private string questGiver;
    private Variant variant;

    public CatRescueQuest(string questGiver, Variant variant) : base(QUEST_NAME, DESCRIPTION)
    {
        this.questGiver = questGiver;
        this.variant = variant;
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

    public static void AddNewCatQuest(GameManager manager, CatRescueQuest oldQuest)
    {
        Variant variant = Variant.NORMAL;

        if (manager.day >= 30 && Random.value < 0.5)
        {
            variant = Variant.LADDER;
        }

        manager.questQueue.Enqueue(new CatRescueQuest(oldQuest.questGiver, variant));
    }

    public override IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        ItemInstance tuna = hero.inventory.Find(item => item.itemData.itemType == ItemData.Type.TUNA);

        if (tuna != null)
        {
            if (variant == Variant.NORMAL)
            {
                hero.inventory.Remove(tuna);
                yield return new DialogueText(hero.heroData.heroName, "Just so you know, I found the cat.");
                yield return new DialogueText("", "You got 50 coins!");
                manager.SetCurrency(manager.currency + 50);
                AddNewCatQuest(manager, this);
            }
            else if (variant == Variant.LADDER)
            {
                ItemInstance ladder = hero.inventory.Find(item => item.itemData.itemType == ItemData.Type.LADDER);

                if (ladder != null)
                {
                    hero.inventory.Remove(tuna);
                    hero.inventory.Remove(ladder);
                    yield return new DialogueText(hero.heroData.heroName, "I found the cat up in a tree somewhere.");
                    yield return new DialogueText("", "You got 75 coins!");
                    manager.SetCurrency(manager.currency + 75);
                    AddNewCatQuest(manager, this);
                }
                else
                {
                    yield return new DialogueText(hero.heroData.heroName, "I found the cat, but it was too high up to reach.");
                    manager.questList.AddQuest(this);
                }
            }
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "I was not able to find that cat you told me about.");
            yield return new DialogueText(hero.heroData.heroName, "You might have better luck if you had something to lure it with.");
            manager.questList.AddQuest(this);
        }

        hero.Idle();
    }
}