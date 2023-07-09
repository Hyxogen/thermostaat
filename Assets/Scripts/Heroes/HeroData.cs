using UnityEngine;

[CreateAssetMenu]
public class HeroData : ScriptableObject
{
    public enum Type
    {
        RANGER,
        KNIGHT,
        BARD,
    }

    public string heroName;
    public string spriteName;
    public Type heroType;
}