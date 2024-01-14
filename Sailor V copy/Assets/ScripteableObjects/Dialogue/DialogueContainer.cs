using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Dialogue Container", menuName = "Dialogue/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    public DialogueNode startNode;
    public List<DialogueNode> nodeList;
    public string testText;

#if UNITY_EDITOR
    [ContextMenu("Add dialogue text")]
    void AddTextNode(string nodeName = null)
    {
        var newNode = ScriptableObject.CreateInstance<DialogueTextNode>();
        newNode.name = nodeName ?? GUID.Generate().ToString();

        newNode.Initialise(this);
        nodeList.Add(newNode);
        AssetDatabase.AddObjectToAsset(newNode, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(newNode);
    }

    [ContextMenu("Add dialogue options")]
    void AddOptionsNode(string nodeName = null)
    {
        var newNode = ScriptableObject.CreateInstance<DialogueOptionsNode>();
        newNode.name = nodeName ?? GUID.Generate().ToString();
        newNode.Initialise(this);
        nodeList.Add(newNode);

        AssetDatabase.AddObjectToAsset(newNode, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(newNode);
    }
#endif
}
