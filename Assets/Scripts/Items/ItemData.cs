using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [System.Serializable]
    public enum Type
    {
        TUNA,
        LADDER,
    }

    public string itemName;
    public Type itemType;
    public Sprite sprite;
    [TextArea]
    public string description;
    public int minCost;
    public int maxCost;
}