using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TooltipProvider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipManager tooltipManager;

    public abstract string GetTooltip();

    private TooltipManager GetTooltipManager()
    {
        if (tooltipManager == null)
        {
            tooltipManager = FindObjectOfType<TooltipManager>();
        }

        return tooltipManager;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetTooltipManager().AddTooltip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetTooltipManager().RemoveTooltip(this);
    }
}