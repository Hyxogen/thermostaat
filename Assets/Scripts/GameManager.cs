using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestListUI questList;
    public ItemInventoryUI itemInventory;
    public DialogueManager dialogueManager;
    public SpriteManager spriteManager;
    public List<HeroInstance> allHeroes;
    public Queue<Quest> questQueue = new();
    public Queue<Quest> completedQuestQueue = new();
    public List<ItemInstance> shopItems;
    public TMPro.TextMeshProUGUI currencyText;
    public TMPro.TextMeshProUGUI dayText;

    public int day { private set; get; } = 0;
    private int currency = 200;

    public int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
            UpdateUI();
        }
    }

    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            currency = value;
            UpdateUI();
        }
    }

    public GameManager()
    {
        questQueue.Enqueue(new SlimeBasementQuest("Stanley"));
        questQueue.Enqueue(new InterestingLocationQuest("Brandon"));
        questQueue.Enqueue(new CatRescueQuest("David", CatRescueQuest.Variant.NORMAL));
    }

    private void Start()
    {
        UpdateUI();
    }

    public string PlayerIdentifier()
    {
        return "John (You)";
    }

    public string PlayerName()
    {
        return "John";
    }

    public void UpdateUI()
    {
        currencyText.text = "" + Currency;
        dayText.text = "" + Day;
    }

    public bool Buy(int cost)
    {
        if (Currency >= cost)
        {
            Currency -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NextDay()
    {
        Day += 1;
        Currency -= 2;

        switch (Day)
        {
            case 25:
                dialogueManager.dialogueQueue.Enqueue(new FindScrollDialogue());
                break;
            case 50:
                questQueue.Enqueue(new DragonQuest());
                break;
            default:
                break;
        }
    }
}
