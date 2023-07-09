using System.Collections.Generic;
using UnityEngine;

public class AnnoyingHeroQuest : Quest
{

    private static string QUEST_NAME = "";
    private static string DESCRIPTION = "";
    private static string SPRITE_NAME = "Peasant02";

    private string questGiver;

    public AnnoyingHeroQuest(string questGiver) : base(QUEST_NAME, DESCRIPTION)
    {
        this.questGiver = questGiver;
    }

    public override IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        string playerIdent = manager.PlayerIdentifier();
        string playerName = manager.PlayerName();

        yield return new DialogueText("", "Someone you don't know is approaching your shop");
        yield return new DialogueText("(Unknown)", "SKIP", SPRITE_NAME);
        yield return new DialogueText("(Unknown)", "SKIP", SPRITE_NAME);
        yield return new DialogueText("(Unknown)", "SKIP", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "?", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "Is everything oka-", SPRITE_NAME);
        yield return new DialogueText("(Unknown)", "SKIP", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "How can I help you?", SPRITE_NAME);
        yield return new DialogueText("(Unknown)", "SKIP. SKIP. SKIP.\nShow me your wares!", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "You don't have eyes or something?", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "You're standing in my sho-", SPRITE_NAME);
        yield return new DialogueText("(Unknown)", "SKIP", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "...", SPRITE_NAME);
        yield return new DialogueText("(Unknown voice from outside)", "Come " + questGiver + "! We're going to the tavern!", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Ugh, they always don't work when you need them", SPRITE_NAME);
        yield return new DialogueText(questGiver, "Coming!", SPRITE_NAME);
        yield return new DialogueText(playerIdent, "*Sighs*");
    }

    public override IEnumerable<IDialogueBase> GiveDialogue(GameManager manager, HeroInstance hero)
    {
        yield return null;
    }

    public override IEnumerable<IDialogueBase> Embark(GameManager manager, HeroInstance hero)
    {
        yield return null;
    }
}