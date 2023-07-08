using System.Collections.Generic;

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
            yield return new DialogueText("", "Nothing happened today.\nYour rent for today is 2 coins.");
        }
        else
        {
            yield return new DialogueText("", "It's getting dark, you should rest.\nYour rent for today is 2 coins.");
        }

        manager.dialogueManager.StartNewDay();
    }
}