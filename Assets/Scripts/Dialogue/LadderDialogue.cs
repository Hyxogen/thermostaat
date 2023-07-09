using System.Collections.Generic;

public class LadderDialogue : IDialogue
{
    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        yield return new DialogueText("", "Looks like somebody placed a ladder that leads up to your roof.");
        yield return new DialogueText("", "I wonder what they're doing up there...");
        yield return new DialogueText("", "You received 1 ladder!");

        manager.itemInventory.AddItem(new ItemInstance(ItemManager.Instance().ladder));
    }
}