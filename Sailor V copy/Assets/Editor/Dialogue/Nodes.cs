using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class EditorNode : Node
{
    public string GUID;
    public string nodeName;
    public DialogueItem targetItem;
    public NodesConst.Type type;
    public List<Port> outputPorts = new();
}

[System.Serializable]
public class BaseNodeData
{
    public string Guid;
    public string title;
    public Vector2 position;
    // public DialogueElement item;
    public NodesConst.Type type;
}


[System.Serializable]
public class NodeLinkData
{
    public string baseNodeGUID;
    public string portName;
    public string targetNodeGUID;
}


