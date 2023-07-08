using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI nameField;
    public TMPro.TextMeshProUGUI dialogueField;

    public GameObject nextButton;
    public GameObject optionAButton;
    public GameObject optionBButton;
    public GameManager gameManager;

    private IDialogueChoice currentChoice;
    private IEnumerator<IDialogueBase> currentDialogue;
    private Queue<IDialogue> dialogueQueue = new();

    void Start()
    {
        Reset();
        StartNewDay();
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (currentChoice != null)
        {
            return;
        }

        NextDialogueAndClearChoice();
    }

    public void StartNewDay()
    {
        if (gameManager.questQueue.Count > 0 && Random.value < 0.2)
        {
            dialogueQueue.Enqueue(gameManager.questQueue.Dequeue());
        }

        if (gameManager.shopItems.Count > 0 && Random.value < 0.2)
        {
            ItemInstance item = gameManager.shopItems[Random.Range(0, gameManager.shopItems.Count)];
            dialogueQueue.Enqueue(new ShopDialogue(item, "Jeffrey"));
        }

        foreach (HeroInstance hero in gameManager.allHeroes)
        {
            if (hero.idleTime == 0)
            {
                dialogueQueue.Enqueue(new TakeQuestDialogue(hero));
            }

            if (hero.questTime == 0)
            {
                dialogueQueue.Enqueue(new QuestDoneDialogue(hero));
            }

            if (hero.idleTime >= 0)
            {
                hero.idleTime -= 1;
            }

            if (hero.questTime >= 0)
            {
                hero.questTime -= 1;
            }
        }

        dialogueQueue.Enqueue(new EndOfDayDialogue(dialogueQueue.Count == 0));
        gameManager.NextDay();
    }

    void NextDialogueAndClearChoice()
    {
        ClearChoice();

        while (currentDialogue == null || !currentDialogue.MoveNext())
        {
            if (dialogueQueue.Count == 0)
            {
                StartNewDay();
            }

            currentDialogue = dialogueQueue.Dequeue().Next(gameManager);
        }

        DisplayDialogue(currentDialogue.Current);
    }

    void ClearDialogue()
    {
        nameField.text = "";
        dialogueField.text = "";
    }

    void ClearChoice()
    {
        optionAButton.SetActive(false);
        optionBButton.SetActive(false);
        nextButton.SetActive(true);
        currentChoice = null;
    }

    void Reset()
    {
        ClearDialogue();
        ClearChoice();
    }

    void DisplayTextDialogue(string actorName, string dialogue)
    {
        nameField.text = actorName;
        dialogueField.text = dialogue;
    }

    void DisplayTextDialogue(DialogueText textDiag)
    {
        DisplayTextDialogue(textDiag.actorName, textDiag.dialogue);
    }

    void DisplayChoiceDialogue(IDialogueChoice option)
    {
        // ClearDialogue();
        optionAButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = option.ADesc();
        optionBButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = option.BDesc();
        optionAButton.SetActive(true);
        optionBButton.SetActive(true);
        nextButton.SetActive(false);
        currentChoice = option;
        //optionAButton.transform.GetComponentInChildren<TMPro.field.text = options[i].description;
    }

    void DisplayDialogue(IDialogueBase dialogue)
    {
        if (dialogue is DialogueText)
        {
            DisplayTextDialogue((DialogueText)dialogue);
        }
        else if (dialogue is IDialogueChoice)
        {
            DisplayChoiceDialogue((IDialogueChoice)dialogue);
        }
        else
        {
            Debug.LogError("Unknown dialogue type");
        }
    }

    public void ChooseA()
    {
        if (currentChoice != null)
        {
            Debug.Log("Chose A");
            currentChoice.OptionA();
            NextDialogueAndClearChoice();
        }
        else
        {
            Debug.LogError("Currently not displaying a option");
        }
    }

    public void ChooseB()
    {
        if (currentChoice != null)
        {
            Debug.Log("Chose B");
            currentChoice.OptionB();
            NextDialogueAndClearChoice();
        }
        else
        {
            Debug.LogError("Currently not displaying a option");
        }
    }
}