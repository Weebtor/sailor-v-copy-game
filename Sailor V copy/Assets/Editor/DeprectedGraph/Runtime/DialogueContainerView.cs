using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Container")]
public class DialogueContainerView : ScriptableObject
{
    public List<NodeLinkData> nodesLinks = new();
    public List<BaseNodeData> nodesData = new();
}
