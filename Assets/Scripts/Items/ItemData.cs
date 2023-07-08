using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [System.Serializable]
    public enum Type
    {
        TUNA,
    }

    public string itemName;
    public Type itemType;
    [TextArea]
    public string description;
    public int minCost;
    public int maxCost;
}