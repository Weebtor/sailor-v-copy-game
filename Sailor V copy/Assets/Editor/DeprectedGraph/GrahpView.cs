using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Ionic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;



public class DialogueGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new(100, 150);

    public DialogueGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogueEditorWindow"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddInitialNodes();
    }


    public void AddInitialNodes()
    {
        AddElement(StartNodeView());
        AddElement(FinishNodeView());
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new();
        ports.ForEach(port =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }
    Port GeneratePort(GraphNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    public GraphNode StartNodeView()
    {
        var node = new GraphNode
        {
            title = "Start",
            GUID = NodesConst.Ids.StartNode,
            nodeName = "Start",
            type = NodesConst.Type.start,
        };
        var generatedOutputPort = GeneratePort(node, Direction.Output);
        generatedOutputPort.portName = "Next";
        node.outputContainer.Add(generatedOutputPort);

        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(100, 200, 1000, 150));

        return node;
    }
    public GraphNode FinishNodeView()
    {
        var node = new GraphNode
        {
            title = "Finish",
            GUID = NodesConst.Ids.FinishNode,
            nodeName = "Finish",
            type = NodesConst.Type.finish,
        };
        var generatedInputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        generatedInputPort.portName = "Finish";

        node.inputContainer.Add(generatedInputPort);

        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(200, 200, 1000, 150));

        return node;
    }
    GraphNode GenerateNodeItem(NodesConst.Type itemType, DialogueElement dialogueItem = null)
    {
        DialogueElement item = dialogueItem ?? itemType switch
        {
            NodesConst.Type.text => new DialogueTextElement(),
            NodesConst.Type.options => new DialogueOptionsElement(),
            _ => null,
        };
        var itemNode = new GraphNode()
        {
            GUID = Guid.NewGuid().ToString(),
            item = item,
            type = itemType,
        };
        return itemNode;
    }

    public GraphNode TextNodeView(string title = "Dialogue", DialogueTextElement loadItem = null)
    {
        var textNode = GenerateNodeItem(NodesConst.Type.text, loadItem);
        textNode.title = title;

        var inputPort = GeneratePort(textNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "input";

        var outputPort = GeneratePort(textNode, Direction.Output);
        outputPort.portName = "output";

        textNode.inputContainer.Add(inputPort);
        textNode.outputContainer.Add(outputPort);

        var boxContainer = new Box();

        Debug.Log($"character: <color=orange>{loadItem?.character}</color>");

        var characterField = new ObjectField("character")
        {
            value = loadItem?.character,
            objectType = typeof(DialogueCharacter),
            label = "Character:"
        };
        characterField.RegisterValueChangedCallback(evt =>
        {
            var item = (DialogueTextElement)textNode.item;
            item.character = (DialogueCharacter)evt.newValue;

        });

        var label = new Label("Dialogue Text");
        var textValueField = new TextField()
        {
            value = loadItem?.dialogueText,
            multiline = true,
            style = { width = 300 }
        };

        boxContainer.Add(characterField);

        boxContainer.Add(label);
        boxContainer.Add(textValueField);
        textValueField.RegisterValueChangedCallback(evt =>
        {
            var item = (DialogueTextElement)textNode.item;
            item.dialogueText = evt.newValue;
        });
        var item = (DialogueTextElement)textNode.item;
        textNode.mainContainer.Add(boxContainer);

        textNode.RefreshExpandedState();
        textNode.RefreshPorts();
        textNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return textNode;
    }

    public GraphNode OptionsNodeView(string title = "Option", DialogueOptionsElement item = null)
    {
        var optionNode = GenerateNodeItem(NodesConst.Type.options);
        optionNode.title = title;

        var inputPort = GeneratePort(optionNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "input";
        optionNode.inputContainer.Add(inputPort);

        var button_addPort = new Button(() => AddOutputPort(optionNode))
        {
            text = "Add Output"
        };
        optionNode.titleContainer.Add(button_addPort);
        optionNode.RefreshExpandedState();
        optionNode.RefreshPorts();
        optionNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return optionNode;
    }

    public void AddDialogueTextNode()
    {
        var dialogueTextNode = TextNodeView();
        AddElement(dialogueTextNode);
    }
    public void AddDialogueOptionsNode()
    {
        var dialogueTextNode = OptionsNodeView();
        AddElement(dialogueTextNode);
    }
    public Port AddOutputPort(GraphNode dialogueNode, string overridenPortName = "")
    {
        var outputPort = GeneratePort(dialogueNode, Direction.Output);
        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;

        string choicePortName = string.IsNullOrEmpty(overridenPortName)
            ? $"Choice {outputPortCount}"
            : overridenPortName;

        var textFieldInput = new TextField
        {
            name = string.Empty,
            value = choicePortName,
            style = {
                width = 100
            }
        };

        textFieldInput.RegisterValueChangedCallback(evt => outputPort.portName = evt.newValue);

        var button_removePort = new Button(() => RemoveOutputPort(dialogueNode, outputPort))
        {
            text = "X",
            style = {
                width = 15
            }
        };
        outputPort.contentContainer.Add(button_removePort);
        outputPort.contentContainer.Add(textFieldInput);

        outputPort.portName = choicePortName;
        dialogueNode.outputContainer.Add(outputPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();

        return outputPort;
    }
    void RemoveOutputPort(GraphNode node, Port generatedPort)
    {
        var targetEdge = edges.ToList().Where(edge =>
            edge.output.portName == generatedPort.portName && edge.output.node == generatedPort.node);
        if (!targetEdge.Any()) return;

        var edge = targetEdge.First();
        edge.input.Disconnect(edge);
        RemoveElement(targetEdge.First());

        node.outputContainer.Remove(generatedPort);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }


}
