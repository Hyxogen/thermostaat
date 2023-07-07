using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI nameField;
    public TMPro.TextMeshProUGUI dialogueField;

    public GameObject[] optionButtons;

    public DialogueTree current;
    private Queue<(string, string)> dialogueQueue = new Queue<(string, string)>();
    private List<DialogueOption> options = new List<DialogueOption>();

    void Start()
    {
        PlayDialogue(current);
    }

    void Update()
    {
        
    }

    public void NextDialogue() {
        if (current == null) {
            Debug.LogWarning("No more dialogue available");
            return;
        }

        if (dialogueQueue.Count > 0) {
            (string name, string text) pair = dialogueQueue.Dequeue();
            Show(pair.name, pair.text);
        } else if (options.Count > 0) {
            ShowOptions();
        } else {
            Clear();
            Show("Me", "No more text to display");
        }
    }

    void Show(string name, string text) {
        nameField.text = name;
        dialogueField.text = text;
    }

    void ShowOptions() {
            for (int i = 0; i < options.Count; ++i) {
                TMPro.TextMeshProUGUI field = optionButtons[i].transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                field.text = options[i].description;
                optionButtons[i].gameObject.SetActive(true);
            }
    }

    void ClearDialogue() {
        nameField.text = "";
        dialogueField.text = "";
    }

    void ClearOptions() {
        options.Clear();
        foreach (GameObject option in optionButtons) {
            option.SetActive(false);
        }
    }

    void Clear() {
        ClearDialogue();
        ClearOptions();
    }

    public void QueueDialogue(DialogueTree tree) {
        foreach (Dialogue dialogue in tree.dialogues) {
            foreach (string text in dialogue.dialogue) {
                dialogueQueue.Enqueue((dialogue.name, text));
            }
        }
    }

    public void PlayDialogue(DialogueTree tree) {
        Clear();

        foreach (DialogueOption option in tree.options) {
            options.Add(option);
        }

        QueueDialogue(tree);
        NextDialogue();
    }
    
    public void ChooseA() {
        PlayDialogue(current.options[0].tree);
    }

    public void ChooseB() {
        PlayDialogue(current.options[1].tree);
    }
}
