using System.Collections.Generic;
using UnityEngine;

public class ShopDialogue : IDialogue
{
    private ItemInstance item;
    private int cost;
    private string merchant;
    private string spriteName;
    private int attempts;

    public ShopDialogue(ItemInstance item, string merchant, int attempts, string spriteName)
    {
        this.item = item;
        this.cost = Random.Range(item.itemData.minCost, item.itemData.maxCost + 1);
        this.merchant = merchant;
        this.spriteName = spriteName;
        this.attempts = attempts;
    }

    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        bool buyItem = false;

        yield return new DialogueText(merchant, "Buy my " + item.itemData.itemName + "!", spriteName);

        yield return new LambdaDialogueChoice("Yes (" + cost + ")", () => buyItem = true, "No", () => buyItem = false, spriteName);

        while (attempts > 1)
        {
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
                yield return new DialogueText(manager.PlayerIdentifier(), "I don't want your " + item.itemData.itemName + ".", spriteName);
                yield return new DialogueText(merchant, "Yes you do!", spriteName);
                yield return new LambdaDialogueChoice("I do? (" + cost + ")", () => buyItem = true, "No I don't!", () => buyItem = false, spriteName);
            }

            attempts -= 1;
        }

        if (buyItem)
        {
            if (manager.Buy(cost))
            {
                manager.itemInventory.AddItem(new ItemInstance(item));
                yield return new DialogueText(manager.PlayerIdentifier(), "Thanks.");
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