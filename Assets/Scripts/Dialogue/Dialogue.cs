using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueBase { }

public struct DialogueText : IDialogueBase
{
    public string actorName;
    public string dialogue;

    public DialogueText(string actorName, string dialogue)
    {
        this.actorName = actorName;
        this.dialogue = dialogue;
    }
}

public interface IDialogueChoice : IDialogueBase
{
    string ADesc();
    string BDesc();
    void OptionA();
    void OptionB();
}

public struct LambdaDialogueChoice : IDialogueChoice
{
    public Action optionA;
    public string descriptionA;
    public Action optionB;
    public string descriptionB;

    public LambdaDialogueChoice(string descriptionA, Action optionA, string descriptionB, Action optionB)
    {
        this.descriptionA = descriptionA;
        this.optionA = optionA;
        this.descriptionB = descriptionB;
        this.optionB = optionB;
    }

    public void OptionA()
    {
        optionA.Invoke();
    }

    public string ADesc()
    {
        return descriptionA;
    }

    public void OptionB()
    {
        optionB.Invoke();
    }

    public string BDesc()
    {
        return descriptionB;
    }
}

public interface IDialogue
{
    public IEnumerator<IDialogueBase> Next(GameManager manager);
}

public class SimpleDialogue : IDialogue
{
    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        string name = "David";
        string you = "You";

        bool savesCat = false;

        yield return new DialogueText(name, "Hey there John");
        yield return new DialogueText(name, "Have you seen my cat Whiskers?");
        yield return new DialogueText(you, "Nope");
        yield return new DialogueText(name, "Darn, was hoping you saw him");
        yield return new DialogueText(name, "Could you catch him again if he's around?");

        yield return new LambdaDialogueChoice("Yeah ofcource", () => savesCat = true, "No, fuck your cat", () => savesCat = false);

        if (savesCat)
        {
            yield return new DialogueText(name, "Thanks man");
        }
        else
        {
            yield return new DialogueText(name, "Understandable, have a great day");
        }
    }
}

public class EndOfDayDialogue : IDialogue
{
    private bool emptyDay;

    public EndOfDayDialogue(bool emptyDay)
    {
        this.emptyDay = emptyDay;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        if (emptyDay)
        {
            yield return new DialogueText("", "Nothing happened today.");
        }
        else
        {
            yield return new DialogueText("", "It's getting dark, you should rest.");
        }

        manager.dialogueManager.StartNewDay();
    }
}

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

        yield return new DialogueText(hero.heroData.heroName, "I'm looking for work.");

        yield return new LambdaDialogueChoice("Start Quest", () => giveQuest = true, "I don't have any", () => giveQuest = false);

        if (giveQuest)
        {
            yield return new DialogueText(hero.heroData.heroName, "You got it boss.");
            // TODO: take quest from quest list
            Quest quest = new CatRescueQuest();
            hero.StartQuest(quest);
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Very well.");
            hero.idleTime = 5;
        }
    }
}

public class QuestDoneDialogue : IDialogue
{
    private HeroInstance hero;

    public QuestDoneDialogue(HeroInstance hero)
    {
        this.hero = hero;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        if (hero.quest.Embark(hero))
        {
            yield return new DialogueText(hero.heroData.heroName, "Just so you know, I finished that thing you wanted me to do.");
        }
        else
        {
            yield return new DialogueText(hero.heroData.heroName, "Fuck fuck fuck fuck fuck fuck fuck.");
        }

        hero.idleTime = 5;
    }
}