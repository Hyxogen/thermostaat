
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueTree")]
public class DialogueTree : ScriptableObject
{
    public Dialogue[] dialogues;

    public DialogueOption[] options;
}
