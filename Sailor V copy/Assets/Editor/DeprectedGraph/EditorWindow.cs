using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditorWindow : EditorWindow
{
    DialogueContainerView dialogueContainerViewCache;
    DialogueGraphView _graphView;



    [MenuItem("Graph/Dialogue Graph")]
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
        rootVisualElement.Remove(_graphView);
    }

    void ConstructGraphView()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };


        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }
    void CreateToolbar()
    {
        Toolbar toolbar = new();
        var dropdown_fileOptions = new ToolbarMenu
        {
            text = "File",
        };
        dropdown_fileOptions.menu.AppendAction("Load file", action => OnLoad());
        dropdown_fileOptions.menu.AppendAction("Save file", action => OnSave());
        dropdown_fileOptions.menu.AppendAction("Save dialogue as", action => { });

        var dropdown_createNode = new ToolbarMenu
        {
            text = "Nodes",
        };
        dropdown_createNode.menu.AppendAction("Add Dialogue Node", action => OnAddDialogueText());
        dropdown_createNode.menu.AppendAction("Add Options Node", action => OnAddOptionsNode());

        toolbar.Add(dropdown_fileOptions);
        toolbar.Add(dropdown_createNode);


        rootVisualElement.Add(toolbar);
    }

    void OnAddDialogueText()
    {
        if (dialogueContainerViewCache == null)
        {
            EditorUtility.DisplayDialog("file not selected", "Please select a valid file first", "Ok");
            return;
        }
        _graphView.AddDialogueTextNode();
    }
    void OnAddOptionsNode()
    {
        if (dialogueContainerViewCache == null)
        {
            EditorUtility.DisplayDialog("file not selected", "Please select a valid file first", "Ok");
            return;
        }
        _graphView.AddDialogueOptionsNode();
    }
    void OnLoad()
    {
        var save = GraphFileUtily.GetInstance(_graphView);
        save.LoadGraph(ref dialogueContainerViewCache);
    }
    void OnSave()
    {
        if (dialogueContainerViewCache == null)
        {
            EditorUtility.DisplayDialog("file not selected", "Please select a valid file", "Ok");
            return;
        }
        var save = GraphFileUtily.GetInstance(_graphView);
        save.SaveGraph(ref dialogueContainerViewCache);
    }
}
