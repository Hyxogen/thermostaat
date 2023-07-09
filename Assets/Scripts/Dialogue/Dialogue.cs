using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueBase {
    string GetSpriteName();
}

public struct DialogueText : IDialogueBase
{
    public string actorName;
    public string dialogue;
    public string spriteName;

    public DialogueText(string actorName, string dialogue)
    {
        this.actorName = actorName;
        this.dialogue = dialogue;
        this.spriteName = null;
    }

    public DialogueText(string actorName, string dialogue, string spriteName)
    {
        this.actorName = actorName;
        this.dialogue = dialogue;
        this.spriteName = spriteName;
    }

    public string GetSpriteName() {
        return this.spriteName;
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
    public string spriteName;

    public LambdaDialogueChoice(string descriptionA, Action optionA, string descriptionB, Action optionB)
    {
        this.descriptionA = descriptionA;
        this.optionA = optionA;
        this.descriptionB = descriptionB;
        this.optionB = optionB;
        this.spriteName = null;
    }

    public LambdaDialogueChoice(string descriptionA, Action optionA, string descriptionB, Action optionB, string spriteName)
    {
        this.descriptionA = descriptionA;
        this.optionA = optionA;
        this.descriptionB = descriptionB;
        this.optionB = optionB;
        this.spriteName = spriteName;
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

    public string GetSpriteName() {
        return this.spriteName;
    }
}

public interface IDialogue
{
    public IEnumerator<IDialogueBase> Next(GameManager manager);
}
