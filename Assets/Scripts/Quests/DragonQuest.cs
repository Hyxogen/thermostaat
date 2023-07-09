using System.Collections.Generic;
using UnityEngine;

public class DragonQuest : Quest
{
    private static string QUEST_NAME = "Slay Dragon";
    private static string DESCRIPTION = "A massive dragon is destroying our buildings and killing our people. It must be defeated.";

    public DragonQuest() : base(QUEST_NAME, DESCRIPTION)
    {
    }

    public override IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        yield return new DialogueText("", "*Woosh* *Woosh* *Wind noises*");
        yield return new DialogueText("", "Leaves and dust race through the streets as a large shadow passes over the village.");
        yield return new DialogueText("", "You look outside and see a massive dragon descending rapidly.");
        yield return new DialogueText("", "The people rush into their homes and lock their doors.");
        yield return new DialogueText("", "As the dragon comes down it grabs a house in its claws.");
        yield return new DialogueText("", "The dragon takes the house and flies off into the distance.");
        yield return new DialogueText("", "You are glad it was not your house.");

        manager.questList.AddQuest(this);
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        yield return new DialogueText(manager.PlayerIdentifier(), "Did you see that massive dragon flying over our city?", hero.heroData.spriteName);
        yield return new DialogueText(hero.heroData.heroName, "Yeah, I remember, what about it?", hero.heroData.spriteName);
        yield return new DialogueText(manager.PlayerIdentifier(), "This village is toast if it stays here.", hero.heroData.spriteName);
        yield return new DialogueText(manager.PlayerIdentifier(), "You have to go and kill it.", hero.heroData.spriteName);
        yield return new DialogueText(hero.heroData.heroName, "What makes you think I can do that?", hero.heroData.spriteName);
        yield return new DialogueText(manager.PlayerIdentifier(), "I don't know, but you'll be rewarded, or whatever.", hero.heroData.spriteName);
        yield return new DialogueText(hero.heroData.heroName, "Uhm, ok, sure.", hero.heroData.spriteName);

        ItemInstance scroll = hero.inventory.Find(item => item.itemData.itemType == ItemData.Type.SCROLL);

        if (scroll != null)
        {
            yield return new DialogueText("", "...");
            yield return new DialogueText(manager.PlayerIdentifier(), "Hey, wait!");
            yield return new DialogueText(hero.heroData.heroName, "?");
            yield return new DialogueText(hero.heroData.heroName, "?", hero.heroData.spriteName);
            yield return new DialogueText(manager.PlayerIdentifier(), "You'll probably want to take this scroll.", hero.heroData.spriteName);
            yield return new DialogueText(hero.heroData.heroName, "Sure.", hero.heroData.spriteName);
        }
    }

    public override IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        ItemInstance scroll = hero.inventory.Find(item => item.itemData.itemType == ItemData.Type.SCROLL);
        string weaponName = hero.GetPreferredWeaponName();

        if (scroll != null)
        {
            yield return new DialogueText("", "You hear a loud explosion in the distance.");
            yield return new DialogueText("", "It's safe to assume the dragon won't be making a comeback.");
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Yeah, so, I had a look at that dragon.", hero.heroData.spriteName);
            yield return new DialogueText(hero.heroData.heroName, "That thing is enormous, by the way.", hero.heroData.spriteName);
            yield return new DialogueText(hero.heroData.heroName, "You're crazy if you think I can take it down with my old" + weaponName + ".", hero.heroData.spriteName);
            yield return new DialogueText(hero.heroData.heroName, "See you around, funny man.", hero.heroData.spriteName);
        }

        hero.Idle();
    }
}