

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditorHelper
{

    public DialogueGraphView targetEditorView;
    List<Edge> Edges => targetEditorView.edges.ToList();
    List<EditorNode> Nodes => targetEditorView.nodes.Cast<EditorNode>().ToList();
    // public DialogueContainer targetDialogueContainer;


    // public void SaveGraph(ref DialogueContainerView dialogueContainer)
    // {
    //     if (!Edges.Any()) return;

    //     dialogueContainer.nodesLinks.Clear();
    //     dialogueContainer.nodesData.Clear();

    //     var connectedPorts = Edges.ToArray();

    //     foreach (var port in connectedPorts)
    //     {
    //         var outputNode = (GraphNode)port.output.node;
    //         var inputNode = (GraphNode)port.input.node;

    //         dialogueContainer.nodesLinks.Add(new NodeLinkData()
    //         {
    //             baseNodeGUID = outputNode.GUID,
    //             portName = port.output.portName,
    //             targetNodeGUID = inputNode.GUID,
    //         });
    //     }
    //     foreach (var dialogueNode in Nodes)
    //     {
    //         var nodeData = new BaseNodeData()
    //         {
    //             Guid = dialogueNode.GUID,
    //             title = dialogueNode.title,
    //             item = dialogueNode.item,
    //             type = dialogueNode.type,
    //             position = dialogueNode.GetPosition().position,
    //         };
    //         dialogueContainer.nodesData.Add(nodeData);
    //         if (nodeData.item is DialogueTextElement item)
    //         {
    //             Debug.Log($"Text:<color=green>{item.dialogueText}</color>");
    //             Debug.Log($"Character:<color=green>{item.character}</color>");
    //         }

    //     }
    //     EditorUtility.SetDirty(dialogueContainer);
    //     AssetDatabase.SaveAssetIfDirty(dialogueContainer);
    //     AssetDatabase.Refresh();
    // }
    public DialogueTree LoadContainer()
    {

        string path = EditorUtility.OpenFilePanel("Select Dialogue Object", "Assets/Data/Dialogues", "asset");
        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("File not selected");
            return null;
        }

        if (path.StartsWith(Application.dataPath))
            path = "Assets" + path[Application.dataPath.Length..];
        var targetDialogueContainer = AssetDatabase.LoadAssetAtPath<DialogueTree>(path);
        if (targetDialogueContainer == null)
        {
            Debug.Log("Dialogue Object not found at: " + path);
            return null;
        }

        Selection.activeObject = targetDialogueContainer;
        // ClearGraphView();
        // StartEditorView();
        return targetDialogueContainer;
        // LoadEdgesView(dialogueContainerViewCache);
    }

    // public void AddTextNode()
    // {
    //     if (dialogueManipulator.target == null)
    //     {
    //         EditorUtility.DisplayDialog("file not selected", "Please select a valid file first", "Ok");
    //         return;
    //     }
    //     var textNode = targetEditorView.AddDialogueTextNode();
    //     // dialogueManipulator.AddTextNode(textNode.GUID);
    //     // targetDialogueContainer.AddTextNode(textNode.GUID);
    // }

    void ClearGraphView()
    {
        Debug.Log("Clear Graph View");
        foreach (var node in Nodes)
        {
            Edges.Where(edge => edge.input.node == node)
               .ToList()
               .ForEach(edge => targetEditorView.RemoveElement(edge));

            targetEditorView.RemoveElement(node);
        }
    }
    // void StartEditorView()
    // {

    //     // Debug.Log("Load nodes");
    //     // foreach (var nodeData in dialogueContainerViewCache.nodesData)
    //     // {
    //     //     Debug.Log($"Node item: {nodeData.item}");
    //     //     GraphNode tempNode = nodeData.type switch
    //     //     {
    //     //         NodesConst.Type.text => _targetGraphView.TextNodeView(nodeData.title, (DialogueTextElement)nodeData.item),
    //     //         NodesConst.Type.options => _targetGraphView.OptionsNodeView(nodeData.title, (DialogueOptionsElement)nodeData.item),
    //     //         NodesConst.Type.start => _targetGraphView.StartNodeView(),
    //     //         NodesConst.Type.finish => _targetGraphView.FinishNodeView(),
    //     //         _ => null,
    //     //     };
    //     //     if (tempNode == null)
    //     //     {
    //     //         Debug.Log($"tempNode: {nodeData.item} {nodeData.Guid}: is null");
    //     //         continue;
    //     //     }
    //     //     tempNode.GUID = nodeData.Guid;
    //     //     tempNode.SetPosition(new Rect(nodeData.position, _targetGraphView.defaultNodeSize));
    //     //     tempNode.RefreshExpandedState();
    //     //     tempNode.RefreshPorts();
    //     //     _targetGraphView.AddElement(tempNode);
    //     // }
    // }
    // void LoadEdgesView(DialogueContainerView dialogueContainerView)
    // {
    //     Debug.Log("Load edges");
    //     foreach (GraphNode currentNode in Nodes)
    //     {
    //         var outputConnections = dialogueContainerView.nodesLinks
    //             .Where(link => link.baseNodeGUID == currentNode.GUID)
    //             .ToList();

    //         foreach (var connection in outputConnections)
    //         {

    //             var targetNode = Nodes.First(node => node.GUID == connection.targetNodeGUID);
    //             Port targetNodeInputPort = targetNode.inputContainer.Q<Port>();

    //             Port currentNodeOutputPort = currentNode.item switch
    //             {
    //                 DialogueOptionsElement => _targetGraphView.AddOutputPort(currentNode, connection.portName),
    //                 _ => currentNode.outputContainer.Q<Port>(),
    //             };

    //             LinkNodes(currentNodeOutputPort, targetNodeInputPort);
    //         }
    //         currentNode.RefreshPorts();
    //         currentNode.RefreshExpandedState();

    //     }
    // }
    // void LinkNodes(Port output, Port input)
    // {
    //     var tempEdge = new Edge
    //     {
    //         output = output,
    //         input = input,
    //     };
    //     tempEdge.input.Connect(tempEdge);
    //     tempEdge.output.Connect(tempEdge);
    //     _targetGraphView.Add(tempEdge);
    // }


    // public void GenerateDialogueObject()
    // {
    //     if (!Edges.Any() || !Nodes.Any()) return;

    //     var dialogueObject = ScriptableObject.CreateInstance<DialogueObject>();

    //     GraphNode startNode = Nodes.First(node => node.type == NodesConst.Type.start);
    //     // GraphNode endNode = Nodes.First(node => node.type == NodesConst.Type.finish);
    //     // var dialoguesNodes = Nodes.Where(node => node.type == NodesConst.Type.text || node.type == NodesConst.Type.options);
    //     // var connectedPorts = Edges.ToArray();
    //     // var outputNode = (GraphNode)port.output.node;
    //     //     var inputNode = (GraphNode)port.input.node;
    //     // var xd = connectedPorts.Where(port => {
    //     //     var baseNode = (GraphNode)port.output.node;
    //     //     return true;
    //     // });
    // }
}