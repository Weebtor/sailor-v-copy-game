using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Dialogue Container", menuName = "Dialogue/Dialogue Tree")]
public class DialogueTree : ScriptableObject
{
    public DialogueItem startItem;
    public List<DialogueItem> items;
    public Vector2 ed_startNodePostion = new(100, 200);

}