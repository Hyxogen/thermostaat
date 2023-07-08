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
            yield return new DialogueText("", "Nothing happened today.");
        }
        else
        {
            yield return new DialogueText("", "It's getting dark, you should rest.");
        }

        manager.dialogueManager.StartNewDay();
    }
}