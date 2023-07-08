using System.Collections.Generic;
using UnityEngine;

public class ShopDialogue : IDialogue
{
    private ItemInstance item;
    private int cost;
    private string merchant;

    public ShopDialogue(ItemInstance item, string merchant)
    {
        this.item = item;
        this.cost = Random.Range(item.itemData.minCost, item.itemData.maxCost + 1);
        this.merchant = merchant;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool buyItem = false;

        yield return new DialogueText(merchant, "Buy my " + item.itemData.itemName + "!");

        yield return new LambdaDialogueChoice("Yes (" + cost + ")", () => buyItem = true, "No", () => buyItem = false);

        if (buyItem)
        {
            if (manager.Buy(cost))
            {
                manager.itemInventory.AddItem(new ItemInstance(item));
                yield return new DialogueText(merchant, "Buy some more!");
                yield return new LambdaDialogueChoice("Yes (" + cost + ")", () => buyItem = true, "No", () => buyItem = false);
            }
            else
            {
                yield return new DialogueText(merchant, "Another broke NPC... I'll be back.");
                yield break;
            }
        }
        else
        {
            yield return new DialogueText(manager.PlayerName(), "I don't want your " + item.itemData.itemName + ".");
            yield return new DialogueText(merchant, "Yes you do!");
            yield return new LambdaDialogueChoice("I do? (" + cost + ")", () => buyItem = true, "No I don't!", () => buyItem = false);
        }

        if (buyItem)
        {
            if (manager.Buy(cost))
            {
                manager.itemInventory.AddItem(new ItemInstance(item));
                yield return new DialogueText(manager.PlayerName(), "Thanks.");
            }
            else
            {
                yield return new DialogueText(merchant, "Another broke NPC... I'll be back.");
                yield break;
            }
        }
        else
        {
            yield return new DialogueText(merchant, "I'll find another NPC.");
        }
    }
}