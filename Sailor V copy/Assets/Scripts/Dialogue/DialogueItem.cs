using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[System.Serializable]
public abstract class DialogueElement { }

[System.Serializable]
public class DialogueTextElement : DialogueElement
{
    public DialogueCharacter character;
    [TextArea(3, 10)] public string dialogueText;
    public string testText;
}
[System.Serializable]
public class DialogueOptionsElement : DialogueElement
{
    public List<string> dialogueOptions;
}
