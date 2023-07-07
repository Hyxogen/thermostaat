using UnityEngine;
using UnityEngine.EventSystems;

public class QuestUI : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI text;
    public QuestListUI questList;
    public QuestInstance quest;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quest != null)
        {
            questList.currentQuest = quest;
        }
    }

    public void SetItem(QuestInstance quest)
    {
        this.quest = quest;

        if (quest != null)
        {
            text.text = quest.questData.questName;
        }
        else
        {
            text.text = "Empty";
        }
    }
}