using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI nameField;
    public TMPro.TextMeshProUGUI dialogueField;

    public GameObject optionAButton;
    public GameObject optionBButton;
    public GameManager gameManager;

    private IDialogueChoice currentChoice;
    private IEnumerator<IDialogueBase> currentDialogue;
    private Queue<IDialogue> dialogueQueue = new Queue<IDialogue>();

    void Start() {
        Reset();
    }

    public void NextDialogue() {
        if (currentChoice != null) {
            return;
        }
        NextDialogueAndClearChoice();
    }

    void NextDialogueAndClearChoice() {
        ClearChoice();
        if (currentDialogue == null) {
            currentDialogue = new SimpleDialogue().Next(gameManager);
        }

        if (currentDialogue.MoveNext()) {
            DisplayDialogue(currentDialogue.Current);
        }
    }

    void ClearDialogue() {
        nameField.text = "";
        dialogueField.text = "";
    }

    void ClearChoice() {
        optionAButton.SetActive(false);
        optionBButton.SetActive(false);
        currentChoice = null;
    }

    void Reset() {
        ClearDialogue();
        ClearChoice();
    }

    void DisplayTextDialogue(string actorName, string dialogue) {
        nameField.text = actorName;
        dialogueField.text = dialogue;
    }

    void DisplayTextDialogue(DialogueText textDiag) {
        DisplayTextDialogue(textDiag.actorName, textDiag.dialogue);
    }

    void DisplayChoiceDialogue(IDialogueChoice option) {
        ClearDialogue();
        optionAButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = option.ADesc();
        optionBButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = option.BDesc();
        optionAButton.SetActive(true);
        optionBButton.SetActive(true);
        currentChoice = option;
        //optionAButton.transform.GetComponentInChildren<TMPro.field.text = options[i].description;
    }

    void DisplayDialogue(IDialogueBase dialogue) {
        if (dialogue is DialogueText) {
            DisplayTextDialogue((DialogueText) dialogue);
        } else if (dialogue is IDialogueChoice) {
            DisplayChoiceDialogue((IDialogueChoice) dialogue);
        } else {
            Debug.LogError("Unknown dialogue type");
        }
    }

    public void ChooseA() {
        if (currentChoice != null) {
            Debug.Log("Chose A");
            currentChoice.OptionA();
            NextDialogueAndClearChoice();
        } else {
            Debug.LogError("Currently not displaying a option");
        }
    }

    public void ChooseB() {
        if (currentChoice != null) {
            Debug.Log("Chose B");
            currentChoice.OptionB();
            NextDialogueAndClearChoice();
        } else {
            Debug.LogError("Currently not displaying a option");
        }
    }
}