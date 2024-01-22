using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Ionic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;



public class DialogueGraphView : GraphView
{
    DialogueEditorHelper editorHelper = new();
    DialogueTreeManipulator containerManipulator = new();

    public static Vector2 defaultNodeSize = new(100, 150);

    EditorNode startNode = null;

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

        graphViewChanged = OnGraphViewChanged;
    }

    public void LoadView()
    {
        var container = editorHelper.LoadContainer();

        if (container == null) return;

        containerManipulator.targetTree = container;
        // Clear();
        ClearGraphView();
        LoadNodes();
        LoadEdges();


    }

    void ClearGraphView()
    {

        List<Edge> edgesView = edges.ToList();
        List<EditorNode> nodesView = nodes.Cast<EditorNode>().ToList();
        if (nodesView.Count == 0 && edgesView.Count == 0) return;

        foreach (var node in nodesView)
        {
            edgesView.Where(edge => edge.input.node == node)
               .ToList()
               .ForEach(edge => RemoveElement(edge));
            RemoveElement(node);
        }
    }
    void LoadNodes()
    {

        startNode = GenerateStartNode();
        AddElement(startNode);
        foreach (var currentItem in containerManipulator.targetTree.items)
        {
            EditorNode nodeView = currentItem switch
            {
                DialogueTextItem item => GenerateTextNode(item),
                DialogueChoiceItem item => GenerateOptionsNode(item),
                _ => null,
            };
            AddElement(nodeView);
        }
    }
    void LoadEdges()
    {
        List<EditorNode> nodesView = nodes.Cast<EditorNode>().ToList();

        // start item
        EditorNode firstNode = nodesView.Find(node => node.targetItem == containerManipulator.targetTree.startItem);
        Port startPort = startNode.outputPorts[0];
        Port initialPort = (Port)firstNode.inputContainer[0];
        LinkNodes(startPort, initialPort);

        foreach (var currentItem in containerManipulator.targetTree.items)
        {
            EditorNode fromNode = nodesView.Find(node => node.targetItem == currentItem);
            switch (currentItem)
            {
                case DialogueTextItem item:
                    {
                        if (item.nextDialogueItem == null)
                            break;
                        EditorNode targetNode = nodesView.Find(node => node.targetItem == item.nextDialogueItem);
                        Port outputPort = fromNode.outputPorts[0];
                        Port inputPort = (Port)targetNode.inputContainer[0];
                        LinkNodes(outputPort, inputPort);
                        break;
                    }
                case DialogueChoiceItem item:
                    {
                        int index = 0;
                        foreach (var choice in item.choices)
                        {
                            if (choice.nextDialogueItem == null)
                            {
                                index++; continue;
                            }
                            EditorNode targetNode = nodesView.Find(node => node.targetItem == choice.nextDialogueItem);
                            Port outputPort = fromNode.outputPorts[index];
                            Port inputPort = (Port)targetNode.inputContainer[0];
                            LinkNodes(outputPort, inputPort);

                            index++;
                        }
                        break;
                    }
            };
        }
    }
    void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input,
        };
        tempEdge.output.Connect(tempEdge);
        tempEdge.input.Connect(tempEdge);

        Add(tempEdge);
    }

    public override List<Port> GetCompatiblePorts(Port outputPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new();
        ports.ForEach(port =>
        {
            if (outputPort != port && outputPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;
    }
    Port GeneratePort(EditorNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        Port port = Port.Create<Edge>(Orientation.Horizontal, portDirection, capacity, typeof(float));
        if (portDirection == Direction.Output)
        {
            node.outputPorts.Add(port);
        }
        return port;
    }


    public EditorNode GenerateStartNode()
    {
        var node = new EditorNode
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
        node.SetPosition(new Rect(containerManipulator.targetTree.ed_startNodePostion, new(1000, 150)));

        return node;
    }
    GraphViewChange OnGraphViewChanged(GraphViewChange change)
    {
        if (change.elementsToRemove != null)
        {
            foreach (GraphElement e in change.elementsToRemove)
            {
                try
                {
                    switch (e)
                    {
                        case Edge edge: containerManipulator.OnDeleteEdge(edge); break;
                        case EditorNode node: containerManipulator.OnDeleteItem(node); break;
                    }
                }
                catch (System.Exception) { }
            }
        }
        if (change.edgesToCreate != null)
        {
            foreach (Edge edge in change.edgesToCreate)
                containerManipulator.OnCreateEdge(edge);
        }
        if (change.movedElements != null)
        {
            foreach (GraphElement e in change.movedElements)
            {
                if (e is EditorNode node)
                    containerManipulator.OnMovedNode(node);
            }
        }

        return change;
    }
    EditorNode GenerateNodeItem(NodesConst.Type itemType, DialogueItem dialogueItem = null)
    {
        DialogueItem item = dialogueItem != null ? dialogueItem : itemType switch
        {
            NodesConst.Type.text => containerManipulator.AddTextItem(Guid.NewGuid().ToString()),
            NodesConst.Type.options => containerManipulator.AddOptionItem(Guid.NewGuid().ToString()),
            _ => null,
        };
        var itemNode = new EditorNode()
        {
            GUID = item.name,
            targetItem = item,
            type = itemType,
        };
        return itemNode;
    }
    EditorNode GenerateTextNode(DialogueTextItem targetItem = null)
    {
        var textNode = GenerateNodeItem(NodesConst.Type.text, targetItem);
        textNode.title = "Dialogue";

        var inputPort = GeneratePort(textNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "input";

        var outputPort = GeneratePort(textNode, Direction.Output);
        outputPort.portName = "output";

        textNode.inputContainer.Add(inputPort);
        textNode.outputContainer.Add(outputPort);

        var boxContainer = new Box();

        var characterField = new ObjectField("character")
        {
            value = targetItem != null ? targetItem.character : null,
            objectType = typeof(CharacterDialogue),
            label = "Character:"
        };
        characterField.RegisterValueChangedCallback(evt =>
        {
            var target = (DialogueTextItem)textNode.targetItem;
            var newCharacter = (CharacterDialogue)evt.newValue;
            containerManipulator.text.UpdateCharacter(target, newCharacter);
        });

        var label = new Label("Dialogue Text");
        var textValueField = new TextField()
        {
            value = targetItem != null ? targetItem.text : null,
            multiline = true,
            style = { width = 300 }
        };

        boxContainer.Add(characterField);

        boxContainer.Add(label);
        boxContainer.Add(textValueField);
        textValueField.RegisterValueChangedCallback(evt =>
        {
            var target = (DialogueTextItem)textNode.targetItem;
            var newText = evt.newValue;
            containerManipulator.text.UpdateText(target, newText);
        });
        textNode.mainContainer.Add(boxContainer);

        textNode.RefreshExpandedState();
        textNode.RefreshPorts();
        Vector2 position = targetItem != null ? targetItem.ed_NodePosition : Vector2.zero;
        textNode.SetPosition(new Rect(position, defaultNodeSize));

        return textNode;
    }
    EditorNode GenerateOptionsNode(DialogueChoiceItem targetItem = null)
    {
        var optionNode = GenerateNodeItem(NodesConst.Type.options, targetItem);
        optionNode.title = "Option";

        var inputPort = GeneratePort(optionNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "input";
        optionNode.inputContainer.Add(inputPort);

        var btn_addPort = new Button(() => AddOutputPort(optionNode))
        {
            text = "Add Output"
        };
        if (targetItem != null)
        {
            foreach (var choiceOption in targetItem.choices)
            {
                AddOutputPort(optionNode, choiceOption);
            }
        }
        optionNode.titleContainer.Add(btn_addPort);
        optionNode.RefreshExpandedState();
        optionNode.RefreshPorts();

        Vector2 position = targetItem != null ? targetItem.ed_NodePosition : Vector2.zero;
        optionNode.SetPosition(new Rect(position, defaultNodeSize));
        Debug.Log($"[{optionNode.GUID}]: Total of ports{optionNode.outputPorts.Count}");
        return optionNode;
    }

    public EditorNode AddDialogueTextNode()
    {
        if (containerManipulator.targetTree == null)
        {
            EditorUtility.DisplayDialog("file not selected", "Please select a valid file first", "Ok");
            return null;
        }
        var dialogueTextNode = GenerateTextNode();
        AddElement(dialogueTextNode);

        return dialogueTextNode;
    }
    public EditorNode AddDialogueOptionsNode()
    {
        if (containerManipulator.targetTree == null)
        {
            EditorUtility.DisplayDialog("file not selected", "Please select a valid file first", "Ok");
            return null;
        }
        var optionNode = GenerateOptionsNode();

        AddElement(optionNode);
        return optionNode;
    }

    Port AddOutputPort(EditorNode node, ChoiceOption defaultChoiceOption = null)
    {
        ChoiceOption choiceOption = defaultChoiceOption ?? containerManipulator.options.GenerateOption((DialogueChoiceItem)node.targetItem);

        Port outputPort = GeneratePort(node, Direction.Output);
        string choicePortName = choiceOption.text;

        var textFieldInput = new TextField
        {
            name = string.Empty,
            value = choiceOption.text,
            style = {
                width = 100
            }
        };

        textFieldInput.RegisterValueChangedCallback(evt =>
        {
            choiceOption.text = evt.newValue;
            // int indexPort = node.outputPorts.FindIndex(port => port == outputPort);
            // node.targetItem
            // Debug.Log($"index port: {indexPort}");
            // outputPort.portName = 
        });

        var button_removePort = new Button(() => RemoveOutputPort(node, outputPort, choiceOption))
        {
            text = "X",
            style = {
                width = 15
            }
        };
        outputPort.contentContainer.Add(button_removePort);
        outputPort.contentContainer.Add(textFieldInput);

        outputPort.portName = choicePortName;
        node.outputContainer.Add(outputPort);
        node.RefreshPorts();
        node.RefreshExpandedState();

        return outputPort;
    }

    void RemoveOutputPort(EditorNode node, Port generatedPort, ChoiceOption optionItem)
    {
        node.outputPorts.Remove(generatedPort);
        var targetItem = (DialogueChoiceItem)node.targetItem;
        containerManipulator.options.RemoveOption(targetItem, optionItem);

        var edgesToDelete = edges.ToList().Where(edge =>
            edge.output.portName == generatedPort.portName && edge.output.node == generatedPort.node);
        // if (!edgesToDelete.Any()) return;


        foreach (var edge in edgesToDelete)
        {
            Debug.Log($"edge [{node.outputPorts.IndexOf(generatedPort)}]: {edge} ");
            edge.output.Disconnect(edge);
            RemoveElement(edge);

        }

        node.outputContainer.Remove(generatedPort);
        // here: revisar metodo delete port (desconectar)
        node.RefreshPorts();
        node.RefreshExpandedState();
    }


}
