using System.Collections.Generic;

public class CompletedQuestDialogue : IDialogue
{
    private Quest quest;

    public CompletedQuestDialogue(Quest quest)
    {
        this.quest = quest;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        foreach (IDialogueBase diag in quest.QuestCompleted(manager))
        {
            yield return diag;
        }
    }
}