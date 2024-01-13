// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(fileName = "Character Dialogue", menuName = "new Dialogue/ Character Dialogue")]
// public class CharacterDialogue : ScriptableObject
// {
//     public string displayName;
//     public Sprite sprite;
// }


// [CreateAssetMenu(fileName = "DialogueContainer", menuName = "new Dialogue/ Dialogue container")]
// public class DialogueContainer : ScriptableObject
// {
//     public DialogueNode startNode;
// }


// [System.Serializable]

// public abstract class DialogueNode { }


// public class TextNode : DialogueNode
// {
//     public CharacterDialogue character;
//     public string Text;
//     public DialogueNode nextNode;
// }

// public class OptionText
// {
//     public string text;
//     public DialogueNode nextNode;
// }
// public class OptionNode : DialogueNode
// {
//     public List<OptionText> options;
// }