using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueChoiceItem : DialogueItem
{
    public List<ChoiceOption> choices = new();
}

[System.Serializable]
public class ChoiceOption
{
    public string text = "--------Default text-----";
    public DialogueItem nextDialogueItem;
}