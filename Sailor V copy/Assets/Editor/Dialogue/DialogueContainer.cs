using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(DialogueContainer))]
public class DialogueContainerEditor : Editor
{
    public DialogueContainer dialogueContainer;
    void OnEnable()
    {
        dialogueContainer = (DialogueContainer)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        dialogueContainer.testText = EditorGUILayout.TextArea("some text", dialogueContainer.testText);
    }

}
