using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueItem : ScriptableObject
{
    public DialogueTree myContainer;
    public List<DialogueItem> inputs = new();
    public Vector2 ed_NodePosition = Vector2.zero;
}