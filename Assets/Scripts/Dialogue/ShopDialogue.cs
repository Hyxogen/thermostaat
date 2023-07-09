using System.Collections.Generic;
using UnityEngine;

public class ShopDialogue : IDialogue
{
    private ItemInstance item;
    private int cost;
    private string merchant;
    private string spriteName;

    public ShopDialogue(ItemInstance item, string merchant, string spriteName)
    {
        this.item = item;
        this.cost = Random.Range(item.itemData.minCost, item.itemData.maxCost + 1);
        this.merchant = merchant;
        this.spriteName = spriteName;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool buyItem = false;

        yield return new DialogueText(merchant, "Buy my " + item.itemData.itemName + "!", spriteName);

        yield return new LambdaDialogueChoice("Yes (" + cost + ")", () => buyItem = true, "No", () => buyItem = false, spriteName);

        if (buyItem)
        {
            if (manager.Buy(cost))
            {
                manager.itemInventory.AddItem(new ItemInstance(item));
                yield return new DialogueText(merchant, "Buy some more!", spriteName);
                yield return new LambdaDialogueChoice("Yes (" + cost + ")", () => buyItem = true, "No", () => buyItem = false, spriteName);
            }
            else
            {
                yield return new DialogueText(merchant, "Another broke NPC... I'll be back.", spriteName);
                yield break;
            }
        }
        else
        {
            yield return new DialogueText(manager.PlayerName(), "I don't want your " + item.itemData.itemName + ".", spriteName);
            yield return new DialogueText(merchant, "Yes you do!");
            yield return new LambdaDialogueChoice("I do? (" + cost + ")", () => buyItem = true, "No I don't!", () => buyItem = false, spriteName);
        }

        if (buyItem)
        {
            if (manager.Buy(cost))
            {
                manager.itemInventory.AddItem(new ItemInstance(item));
                yield return new DialogueText(manager.PlayerName(), "Thanks.", spriteName);
            }
            else
            {
                yield return new DialogueText(merchant, "Another broke NPC... I'll be back.", spriteName);
                yield break;
            }
        }
        else
        {
            yield return new DialogueText(merchant, "I'll find another NPC.", spriteName);
        }
    }
}