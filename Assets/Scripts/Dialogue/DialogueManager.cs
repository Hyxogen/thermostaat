using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI nameField;
    public TMPro.TextMeshProUGUI dialogueField;
    public Image npcImage;

    public GameObject nextButton;
    public GameObject optionAButton;
    public GameObject optionBButton;
    public GameManager gameManager;
    public float typeSpeed = 0.04f;
    public AudioSource audioSource;

    private IDialogueChoice currentChoice;
    private IEnumerator<IDialogueBase> currentDialogue;
    private Queue<IDialogue> dialogueQueue = new();
    private Coroutine typingCoroutine;
    private bool skipTyping = false;
    private bool isTyping = false;
    private Dictionary<int, List<IDialogue>> scheduledDialogues = new();

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
        if (isTyping) {
            skipTyping = true;
            return;
        }

        NextDialogueAndClearChoice();
    }

    public void ScheduleDialogue(int day, IDialogue dialogue)
    {
        if (!scheduledDialogues.ContainsKey(day))
        {
            scheduledDialogues.Add(day, new());
        }
        scheduledDialogues[day].Add(dialogue);
    }

    public void StartNewDay()
    {
        List<IDialogue> scheduled = scheduledDialogues.GetValueOrDefault(gameManager.day);
        if (scheduled != null)
        {
            foreach (IDialogue dialogue in scheduled)
            {
                dialogueQueue.Enqueue(dialogue);
            }
            scheduledDialogues.Remove(gameManager.day);
        }

        if (gameManager.questQueue.Count > 0 && Random.value < 0.2)
        {
            dialogueQueue.Enqueue(gameManager.questQueue.Dequeue());
        }

        if (gameManager.completedQuestQueue.Count > 0 && Random.value < 0.2)
        {
            dialogueQueue.Enqueue(new CompletedQuestDialogue(gameManager.completedQuestQueue.Dequeue()));
        }

        if (gameManager.shopItems.Count > 0 && Random.value < 0.1)
        {
            ItemInstance item = gameManager.shopItems[Random.Range(0, gameManager.shopItems.Count)];

            if (Random.value < 0.4)
            {
                dialogueQueue.Enqueue(new ShopDialogue(item, "Jeffrey", 2, "Peasant01"));
            }
            else
            {
                dialogueQueue.Enqueue(new ShopDialogue(item, "Jeffrey", 1, "Peasant01"));
            }
        }

        if (gameManager.itemInventory.items.Find(item => item.itemData.itemType == ItemData.Type.LADDER) == null && Random.value < 0.1)
        {
            dialogueQueue.Enqueue(new LadderDialogue());
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

    IEnumerator TypeText(string dialogue, float speed)
    {
        isTyping = true;
        foreach (char ch in dialogue.ToCharArray())
        {
            if (skipTyping) {
                skipTyping = false;
                dialogueField.text = dialogue;
                break;
            }
            dialogueField.text += ch;
            if (audioSource != null && !audioSource.isPlaying) {
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.Play();
            }
            yield return new WaitForSeconds(speed);
        }
        isTyping = false;
    }

    void DisplayTextDialogue(string actorName, string dialogue)
    {
        ClearDialogue();
        nameField.text = actorName;
        //dialogueField.text = dialogue;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(dialogue, typeSpeed));
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

    void DisplayImage(string spriteName)
    {
        if (spriteName == null)
        {
            npcImage.gameObject.SetActive(false);
        }
        else
        {
            NamedSprite sprite = gameManager.spriteManager.GetSprite(spriteName);

            if (sprite == null)
            {
                Debug.LogError("Could not find sprite: " + spriteName);
                npcImage.gameObject.SetActive(false);
            }
            else 
            {
                npcImage.sprite = sprite.sprite;
                npcImage.gameObject.SetActive(true);
            }
        }
    }

    void DisplayDialogue(IDialogueBase dialogue)
    {
        DisplayImage(dialogue.GetSpriteName());
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