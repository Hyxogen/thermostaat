using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    private List<TooltipProvider> tooltips = new();
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void AddTooltip(TooltipProvider tooltip)
    {
        tooltips.Add(tooltip);
        text.text = tooltip.GetTooltip();
    }

    public void RemoveTooltip(TooltipProvider tooltip)
    {
        tooltips.Remove(tooltip);

        if (tooltips.Count > 0)
        {
            text.text = tooltips[tooltips.Count - 1].GetTooltip();
        }
        else
        {
            text.text = "";
        }
    }
}
