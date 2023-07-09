using System.Collections.Generic;
using UnityEngine;

public class InterestingLocationQuest : Quest
{

    private static string QUEST_NAME = "Interesting location";
    private static string DESCRIPTION = "Some interesting activity has been taking place at this location";
    private static string SPRITE_NAME = "Peasant01";

    private string questGiver;

    public InterestingLocationQuest(string questGiver) : base(QUEST_NAME, DESCRIPTION)
    {
        this.questGiver = questGiver;
    }

    public override IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();

        yield return new DialogueText("(Unknown)", "G'day", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "Hi, how can I help you?", SPRITE_NAME);
        //yield return new DialogueText("(Unknown)", "Just wondering where the adventures guild is. I'm new to town", SPRITE_NAME);
        yield return new DialogueText(questGiver, "My name is " + questGiver + ". Are those creatures in the forest near town normal?", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "I'm " + playerName + ".\nCreatures?", SPRITE_NAME);
        yield return new DialogueText(questGiver, "On my way here, walking through the forest, I came across this weird sound", SPRITE_NAME);
        yield return new DialogueText(questGiver, "When I followed the sound I saw all kinds of creatures walking around a house", SPRITE_NAME);
        yield return new DialogueText(questGiver, "When I saw that I bolted away", SPRITE_NAME);
        yield return new DialogueText(questGiver, "I couldn't get a good look at em but I'm pretty sure I saw a bear", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "That doesn't sound normal.\nAt least I've never come across that", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Probably best to send a ranger or druid to that place before they actually hurt people", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Could you point me into the direction of the adventurers guild?");

        bool takeQuest = false;
        yield return new LambdaDialogueChoice("*point in right direction*", () => takeQuest = false, "*point in wrong direction*", () => takeQuest = true, SPRITE_NAME);

        yield return new DialogueText(questGiver, "Cheers! See you around", SPRITE_NAME);
        if (takeQuest){
            manager.questList.AddQuest(this);
        }
        //yield return new DialogueText(questGiver, "Anyway, thanks for the help! I'll ")
        //yield return new DialogueText(playerIdent, "", SPRITE_NAME);

        //yield return new DialogueText(playerIdent, "*pointing at a street* If you walk down that street it'll be straight in front of you", SPRITE_NAME);
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();
        string heroName = hero.heroData.heroName;
        string heroSprite = hero.heroData.spriteName;

        yield return new DialogueText(playerIdent, "Yeah, I heard of this interesting place", heroSprite);
        yield return new DialogueText(playerIdent, "How about I tell you where it is and we split the loot in half", heroSprite);
        yield return new DialogueText(heroName, "Hhmmm...", heroSprite);
        yield return new DialogueText(heroName, "Sounds like a good idea", heroSprite);
    }
    
    public override IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();
        string heroName = hero.heroData.heroName;
        string heroSprite = hero.heroData.spriteName;

        //TODO only succeed if hero is ranger
        if (heroName == "Aimer")
        {
            yield return new DialogueText(heroName, "Oh boy, those animals were NOT friendly", heroSprite);
            yield return new DialogueText(heroName, "We found some kind of magical orb that was emitting magic", heroSprite);
            yield return new DialogueText(heroName, "Perhaps that was causing the animals to go wild?", heroSprite);
            yield return new DialogueText(heroName, "It doesn't seem that someone used that house in the last few years", heroSprite);
            yield return new DialogueText(heroName, "We found some other stuffs that could easily be traded for coin", heroSprite);
            yield return new DialogueText(heroName, "Honestly, we don't really care for the magic orb", heroSprite);
            yield return new DialogueText(heroName, "Which do you want?", heroSprite);

            bool takeOrb = false;
            yield return new LambdaDialogueChoice("The magic orb", () => takeOrb = true, "The coin", () => takeOrb = false, heroSprite);

            if (takeOrb)
            {
                yield return new DialogueText(heroName, "Be carefull with that thing", heroSprite);
                manager.itemInventory.AddItem(ItemManager.Instance().orb);
                yield return new DialogueText("", "You received a magic orb");
            }
            else
            {
                yield return new DialogueText(heroName, "I guess we'll take the fun stuff...", heroSprite);
                int coin = Random.Range(50, 60);
                manager.Currency += coin;
                yield return new DialogueText("", "You received " + coin + " coins!");
            }
            yield return new DialogueText(heroName, "See ya around", heroSprite);

            hero.idleTime = 7;
        }
        else
        {
            yield return new DialogueText(heroName, "Thanks a lot man...", heroSprite);
            yield return new DialogueText(playerIdent, "?", heroSprite);
            yield return new DialogueText(heroName, "You could've at least told us about those animals", heroSprite);
            yield return new DialogueText(heroName, "We didn't stand a chance", heroSprite);
            yield return new DialogueText(heroName, "We lost some of our gear as well", heroSprite);
            yield return new DialogueText(heroName, "*slowly drawing his weapon* You better be providing replacements for us", heroSprite);

            bool providesCoin = false;
            int cost = 70;

            yield return new LambdaDialogueChoice("Provide coin (" + cost + ")", () => providesCoin = true, "Refuse", () => providesCoin = false, heroSprite);
            if (providesCoin)
            {
                yield return new DialogueText(heroName, "Thanks for you quick undertanding", heroName);
                manager.Currency -= cost;
                yield return new DialogueText("", "You expended " + cost + " coins!");
            }
            else
            {
                yield return new DialogueText(heroName, "You better watch your back", heroName);
                hero.idleTime = 20;
            }
            //yield return new DialogueText(heroName, "Which you will ofcourse pay the half of", heroSprite);
        }
    }
}