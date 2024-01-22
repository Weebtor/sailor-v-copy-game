
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.UIElements;
using System;
using JetBrains.Annotations;

public class DialogueTreeManipulator
{
    public DialogueTree targetTree;
    public Text text = new();
    public Options options = new();

    public class Text
    {
        public void UpdateText(DialogueTextItem targetItem, string newText)
        {
            targetItem.text = newText;
            // EditorUtility.SetDirty(targetItem);
            // AssetDatabase.SaveAssets();
        }
        public void UpdateCharacter(DialogueTextItem targetItem, CharacterDialogue newCharacter)
        {
            targetItem.character = newCharacter;

            EditorUtility.SetDirty(targetItem);
            AssetDatabase.SaveAssets();
        }
    }
    public class Options
    {
        public ChoiceOption GenerateOption(DialogueChoiceItem targetItem)
        {
            ChoiceOption option = new();
            targetItem.choices.Add(option);
            return option;
        }
        public void RemoveOption(DialogueChoiceItem optionsItem, ChoiceOption option)
        {
            optionsItem.choices.Remove(option);
        }

        public void UpdateOption() { }
    }
    public void SetTarget(DialogueTree target)
    {
        this.targetTree = target;
    }
    private void InitialiseItem(DialogueItem item)
    {
        item.myContainer = targetTree;
    }
    public DialogueTextItem AddTextItem(string name)
    {
        Debug.Log("Adding textnode");
        var newItem = ScriptableObject.CreateInstance<DialogueTextItem>();
        newItem.name = name;
        InitialiseItem(newItem);

        targetTree.items.Add(newItem);
        AssetDatabase.AddObjectToAsset(newItem, targetTree);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(targetTree);
        EditorUtility.SetDirty(newItem);
        return newItem;
    }
    public DialogueChoiceItem AddOptionItem(string name)
    {
        var newItem = ScriptableObject.CreateInstance<DialogueChoiceItem>();
        newItem.name = name;
        InitialiseItem(newItem);

        targetTree.items.Add(newItem);

        AssetDatabase.AddObjectToAsset(newItem, targetTree);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(targetTree);
        EditorUtility.SetDirty(newItem);
        return newItem;
    }
    public void OnCreateEdge(Edge edge)
    {
        var outputNode = (EditorNode)edge.output.node;
        var inputNode = (EditorNode)edge.input.node;

        string outputId = outputNode.GUID;

        if (outputId == NodesConst.Ids.StartNode)
            SetStartItem(inputNode.targetItem);
        else
            LinkItems(edge);

        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(targetTree);

    }
    void SetStartItem(DialogueItem startItem)
    {
        Debug.Log(startItem);
        targetTree.startItem = startItem;
    }
    void LinkItems(Edge edge)
    {
        var outputNode = (EditorNode)edge.output.node;
        var inputNode = (EditorNode)edge.input.node;

        DialogueItem fromItem = outputNode.targetItem;
        DialogueItem toItem = inputNode.targetItem;

        switch (fromItem)
        {
            case DialogueTextItem item:
                item.nextDialogueItem = inputNode.targetItem;
                break;
            case DialogueChoiceItem item:
                int portIndex = outputNode.outputPorts.FindIndex(port => port == edge.output);
                item.choices[portIndex].nextDialogueItem = toItem;
                break;
        }
        toItem.inputs.Add(fromItem);

    }
    public void OnDeleteEdge(Edge edge)
    {
        var outputNode = (EditorNode)edge.output.node;
        var inputNode = (EditorNode)edge.input.node;
        var outputItem = outputNode.targetItem;
        var inputItem = inputNode.targetItem;

        int inputIndex = inputItem.inputs.FindIndex(item => item == outputItem);
        if (inputIndex >= 0)
            inputItem.inputs[inputIndex] = null;

        switch (outputItem)
        {
            case DialogueTextItem item:
                item.nextDialogueItem = null;
                break;
            case DialogueChoiceItem item:
                var fromNode = (EditorNode)edge.output.node;
                int portIndex = fromNode.outputPorts.FindIndex(port => port == edge.output);
                item.choices[portIndex] = null;
                break;
        }
    }
    public void OnDeleteItem(EditorNode node)
    {
        Debug.Log($"Item deleted {node.GUID}");
        DialogueItem item = targetTree.items.Find(item => item.name == node.GUID);
        targetTree.items.Remove(item);
        Undo.DestroyObjectImmediate(item);
        AssetDatabase.SaveAssets();
        Debug.Log("Delete node");
    }
    public void OnMovedNode(EditorNode node)
    {
        Vector2 newPostion = node.GetPosition().position;
        if (node.GUID == NodesConst.Ids.StartNode)
            targetTree.ed_startNodePostion = newPostion;
        else
            node.targetItem.ed_NodePosition = newPostion;
    }
}
