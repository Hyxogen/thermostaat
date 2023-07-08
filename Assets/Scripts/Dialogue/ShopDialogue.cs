using System.Collections.Generic;

public class ShopDialogue : IDialogue
{
    private ItemInstance item;
    private string merchant;

    public ShopDialogue(ItemInstance item, string merchant)
    {
        this.item = item;
        this.merchant = merchant;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool buyItem = false;

        yield return new DialogueText(merchant, "Buy my " + item.itemData.itemName + "!");

        yield return new LambdaDialogueChoice("Yes", () => buyItem = true, "No", () => buyItem = false);

        if (buyItem)
        {
            manager.itemInventory.AddItem(new ItemInstance(item));
            yield return new DialogueText(manager.PlayerName(), "Sure, thanks!");
            yield return new DialogueText(merchant, "Buy some more!");
            yield return new LambdaDialogueChoice("Yes", () => buyItem = true, "No", () => buyItem = false);
        }
        else
        {
            yield return new DialogueText(manager.PlayerName(), "I don't want your " + item.itemData.itemName + ".");
            yield return new DialogueText(merchant, "Yes you do!");
            yield return new LambdaDialogueChoice("You're right, I do.", () => buyItem = true, "No I don't!", () => buyItem = false);
        }

        if (buyItem)
        {
            manager.itemInventory.AddItem(new ItemInstance(item));
            yield return new DialogueText(manager.PlayerName(), "Thanks.");
        }
        else
        {
            yield return new DialogueText(merchant, "I'll find another NPC.");
            yield return new DialogueText(merchant, "Your loss!");
        }
    }
}