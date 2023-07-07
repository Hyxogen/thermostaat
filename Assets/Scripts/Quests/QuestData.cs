using UnityEngine;

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    public string questName;
    [TextArea]
    public string description;
}