using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditorWindow : EditorWindow
{
    // DialogueContainerView dialogueContainerViewCache;
    DialogueGraphView graphView;

    [MenuItem("Dialogue/GraphView Window")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueEditorWindow>();
        window.titleContent = new GUIContent("Dialogue Graph Window");
    }

    void OnEnable()
    {
        ConstructGraphView();
        CreateToolbar();
    }
    void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    void ConstructGraphView()
    {
        graphView = new DialogueGraphView { name = "Dialogue Graph" };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }
    void CreateToolbar()
    {
        Toolbar toolbar = new();
        var dropdown_fileOptions = new ToolbarMenu
        {
            text = "File",
        };
        dropdown_fileOptions.menu.AppendAction("Select Dialogue Container", action => OnLoad());
        // dropdown_fileOptions.menu.AppendAction("Save file", action =>
        // {
        //     // OnSave();
        // });
        // dropdown_fileOptions.menu.AppendAction("Save dialogue as", action => { });

        var dropdown_createNode = new ToolbarMenu
        {
            text = "Nodes",
        };
        dropdown_createNode.menu.AppendAction("Add Dialogue Node", action => AddTextNode());
        dropdown_createNode.menu.AppendAction("Add Options Node", action => AddOptionNode());

        toolbar.Add(dropdown_fileOptions);
        toolbar.Add(dropdown_createNode);


        rootVisualElement.Add(toolbar);
    }
    void OnLoad() => graphView.LoadView();
    void AddTextNode() => graphView.AddDialogueTextNode();
    void AddOptionNode() => graphView.AddDialogueOptionsNode();


}
