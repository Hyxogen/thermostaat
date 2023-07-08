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
