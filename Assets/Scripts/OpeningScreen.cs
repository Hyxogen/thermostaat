using UnityEngine;
using UnityEngine.EventSystems;

public class OpeningScreen : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartNewDay();
        dialogueManager.NextDialogue();
    }
}
