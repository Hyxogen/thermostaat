using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueBase {}

public struct DialogueText : IDialogueBase {
    public string actorName;
    public string dialogue;

    public DialogueText(string actorName, string dialogue) {
        this.actorName = actorName;
        this.dialogue = dialogue;
    }
}

public interface IDialogueChoice : IDialogueBase {
    string ADesc();
    string BDesc();
    void OptionA();
    void OptionB();
}

public struct LambdaDialogueChoice : IDialogueChoice {
    public Action optionA;
    public string descriptionA;
    public Action optionB;
    public string descriptionB;

    public LambdaDialogueChoice(string descriptionA, Action optionA, string descriptionB, Action optionB) {
        this.descriptionA = descriptionA;
        this.optionA = optionA;
        this.descriptionB = descriptionB;
        this.optionB = optionB;
    }

    public void OptionA() {
        optionA.Invoke();
    }

    public string ADesc() {
        return descriptionA;
    }

    public void OptionB() {
        optionB.Invoke();
    }

    public string BDesc() {
        return descriptionB;
    }
}

public interface IDialogue {
    public IEnumerator<IDialogueBase> Next(GameManager manager);
}

public class SimpleDialogue : IDialogue {
    public IEnumerator<IDialogueBase> Next(GameManager manager) {
        string name = "David";
        string you = "You";

        bool saves_cat = false;

        yield return new DialogueText(name, "Hey there John");
        yield return new DialogueText(name, "Have you seen my cat Whiskers?");
        yield return new DialogueText(you, "Nope");
        yield return new DialogueText(name, "Darn, was hoping you saw him");
        yield return new DialogueText(name, "Could you catch him again if he's around?");

        yield return new LambdaDialogueChoice("Yeah ofcource", () => saves_cat = true, "No, fuck your cat", () => saves_cat = false);

        if (saves_cat) {
            //manager.AddQuest(new CatRescueQuest());
            yield return new DialogueText(name, "Thanks man");
        } else {
            yield return new DialogueText(name, "Understandable, have a great day");
        }
    }
}

public class HeroDialogue : IDialogue {
    public IEnumerator<IDialogueBase> Next(GameManager manager) {
        string name = "Matthew";
        string you = "You";

        yield return new DialogueText("(Unknown)", "Good day sir!");
    }
}