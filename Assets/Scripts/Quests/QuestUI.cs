using UnityEngine;
using UnityEngine.EventSystems;

public class QuestUI : TooltipProvider, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI text;
    public QuestListUI questList;
    public Quest quest;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quest != null)
        {
            questList.SetCurrentQuest(quest);
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

    public void Select(bool selected)
    {
        if (selected)
        {
            text.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else
        {
            text.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

    public override string GetTooltip()
    {
        return quest.description;
    }
}