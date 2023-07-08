using UnityEngine;
using UnityEngine.EventSystems;

public class QuestUI : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI text;
    public QuestListUI questList;
    public Quest quest;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quest != null)
        {
            questList.currentQuest = quest;
        }
    }

    public void SetItem(Quest quest)
    {
        this.quest = quest;

        if (quest != null)
        {
            text.text = quest.questName;
        }
        else
        {
            text.text = "Empty";
        }
    }
}