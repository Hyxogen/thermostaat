using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestListUI questList;
    public ItemInventoryUI itemInventory;
    public DialogueManager dialogueManager;
    public List<HeroInstance> allHeroes;
    public Queue<Quest> questQueue = new();
    public List<ItemInstance> shopItems;
    public TMPro.TextMeshProUGUI currencyText;
    public TMPro.TextMeshProUGUI dayText;
    public int currency = 100;
    public int day = 0;

    public GameManager()
    {
        questQueue.Enqueue(new CatRescueQuest("David"));
    }

    private void Start()
    {
        UpdateUI();
    }

    public string PlayerName()
    {
        return "You";
    }

    public void UpdateUI()
    {
        currencyText.text = "" + currency;
        dayText.text = "" + day;
    }

    public void SetCurrency(int currency)
    {
        this.currency = currency;
        UpdateUI();
    }

    public bool Buy(int cost)
    {
        if (currency >= cost)
        {
            SetCurrency(currency - cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NextDay()
    {
        day += 1;
        currency -= 2;
        UpdateUI();
    }
}
