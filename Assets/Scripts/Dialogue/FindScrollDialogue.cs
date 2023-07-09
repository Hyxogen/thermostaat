using System.Collections.Generic;

public class FindScrollDialogue : IDialogue
{
    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        yield return new DialogueText("", "A person walks into your store and starts dropping random items.");
        yield return new DialogueText(manager.PlayerIdentifier(), "Hey, you! Stop that!");
        yield return new DialogueText(manager.PlayerIdentifier(), "This is a reputable store, not some garbage dump!");
        yield return new DialogueText("", "They walk off without saying a word...");
        yield return new DialogueText("", "Among the trash, you find an interesting scroll.");

        manager.itemInventory.AddItem(new ItemInstance(ItemManager.Instance().scroll));
    }
}