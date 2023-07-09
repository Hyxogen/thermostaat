using System.Collections.Generic;
using UnityEngine;

public class SlimeBasementQuest : Quest
{

    private static string QUEST_NAME = "Acidious business";
    private static string DESCRIPTION = "Get me some acid!";
    private static string SPRITE_NAME = "Peasant01";

    private string questGiver;

    public SlimeBasementQuest(string questGiver) : base(QUEST_NAME, DESCRIPTION)
    {
        this.questGiver = questGiver;
    }

    public override IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();

        yield return new DialogueText("(Unknown)", "(From far) " + playerName.ToUpper() + "!!");
        yield return new DialogueText(playerIdent, "(?)");
        yield return new DialogueText("(Unknown)", "(Getting closer) " + playerName.ToUpper() + "!!!!");
        yield return new DialogueText(playerIdent, "(This voice...)");
        yield return new DialogueText(questGiver, playerName + "! Do you have some acid for me?!?", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "Acid? What in the lord's name do you need acid for?", SPRITE_NAME);
        yield return new DialogueText(questGiver, "They're getting everywhere! Please give me some acid!", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "Who's getting everywhere?", SPRITE_NAME);
        yield return new DialogueText(questGiver, "They're so gross *rills*, please give me some acid right now!", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "What are you talking about", SPRITE_NAME);
        yield return new DialogueText(questGiver, "*Reaching over the counter* I know you got it, please give it to me!", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "*grabbing " + questGiver + "'s arm* Could you just anser my question for pete's sake!", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Slimes ofcourse! Now give me that acid! *attempting to reach futher*", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "Could you not go through my stuff thank you very much?!", SPRITE_NAME);

        ItemInstance acid = manager.itemInventory.GetItem(ItemData.Type.ACID);

        if (acid == null)
        {
            yield return new DialogueText(playerIdent, "I don't have acid at the moment anyway", SPRITE_NAME);
            yield return new DialogueText(questGiver, "What?! And you call yourself a merchant?!", SPRITE_NAME);
            yield return new DialogueText(playerIdent, "(...)", SPRITE_NAME);

            bool takesQuest = false;
            yield return new LambdaDialogueChoice("I'll see what I can do ", () => takesQuest = true, "Go look someplace else for your acid", () => takesQuest = false, SPRITE_NAME);

            if (takesQuest)
            {
                yield return new DialogueText(questGiver, "I knew I could count on you!", SPRITE_NAME);
                yield return new DialogueText(questGiver, "But please hurry up!", SPRITE_NAME);
                manager.questList.AddQuest(this);
            }
            else
            {
                yield return new DialogueText(questGiver, "I guess I will!", SPRITE_NAME);
                yield return new DialogueText(questGiver, "*walks of in anger*", SPRITE_NAME);
            }
        }
        else
        {
            bool sellsAcid = false;

            yield return new LambdaDialogueChoice("Luckily for you I have some acid lying around", () => sellsAcid = true, "Sorry, can't help you", () => sellsAcid = false);

            if (sellsAcid)
            {
                int highPrice = 100, lowPrice = 10;
                int price = highPrice;
                while (true)
                {
                    yield return new LambdaDialogueChoice("How about " + highPrice + "?", () => price = highPrice, "How about " + lowPrice + "?", () => price = lowPrice);
                    
                    if (price > 50 && Random.value < 0.1)
                    {
                        yield return new DialogueText(questGiver, "Come on man! Please don't try to rip me of now!", SPRITE_NAME);
                        break;
                    } else if (price > 50)
                    {
                        highPrice -= 30;
                    }
                    else
                    {
                        break;
                    }
                }
                yield return new DialogueText(questGiver, "It's a deal!", SPRITE_NAME);
                manager.Currency += price;
                manager.itemInventory.RemoveItem(acid);
            }
            else
            {
                yield return new DialogueText(questGiver, "And you call yourself a merchant?!", SPRITE_NAME);
                yield return new DialogueText(questGiver, "*runs off*", SPRITE_NAME);
            }
        }
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        yield return new DialogueText(manager.PlayerIdentifier(), "Hey, could you find me some acid?", hero.heroData.spriteName);
        yield return new DialogueText(hero.heroData.heroName, "Sure thing boss.", hero.heroData.spriteName);
    }

    public override IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();

        yield return new DialogueText("(Unknown)", "Pssst");
        yield return new DialogueText("(Unknown)", "*silent voice* Hey");
        yield return new DialogueText(playerIdent, "(?)");
        yield return new DialogueText(hero.heroData.heroName, "I found the stuffs", hero.heroData.spriteName);
        yield return new DialogueText(hero.heroData.heroName, "You arent like using this personally, right?", hero.heroData.spriteName);

        yield return new DialogueText("", "You received 1 bottle of acid!", hero.heroData.spriteName);
        manager.itemInventory.AddItem(new ItemInstance(ItemManager.Instance().acid));

        yield return new DialogueText(hero.heroData.heroName, "Anyway, I'm out of here. Holler me when you need me again", hero.heroData.spriteName);

        hero.Idle();
    }
    
    public override IEnumerable<IDialogueBase> QuestCompleted(GameManager manager)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();

        yield return new DialogueText(questGiver, playerName + "! Do you have the acid for me yet?!", SPRITE_NAME);
        yield return new DialogueText(questGiver, "The slimes are starting to climb out of my basement!", SPRITE_NAME);

        bool sellsAcid = false;
        int price = 20;
        yield return new LambdaDialogueChoice("Yes (+" + price + ")", () => sellsAcid = true, "I couldn't find it", () => sellsAcid = false, SPRITE_NAME);

        if (sellsAcid)
        {
            yield return new DialogueText(questGiver, "You're amazing " + playerName + "!", SPRITE_NAME);
            yield return new DialogueText(questGiver, "*rushing off* Gonna go finally clean my basement!", SPRITE_NAME);
            manager.Currency += price;
            yield return new DialogueText("", "You received " + price + " coins!");
        }
        else
        {
            yield return new DialogueText(questGiver, "If you can't get it, who else can?!", SPRITE_NAME);
            yield return new DialogueText(questGiver, "*rushing off* Perhaps Peter can help me", SPRITE_NAME);
        }
    }
}