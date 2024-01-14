using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class DialogueNode : ScriptableObject
{
    public DialogueContainer myContainer;

#if UNITY_EDITOR
    public void Initialise(DialogueContainer container)
    {
        myContainer = container;
    }
    [ContextMenu("Destroy this")]
    void Delete()
    {
        myContainer.nodeList.Remove(this);
        Undo.DestroyObjectImmediate(this);
        AssetDatabase.SaveAssets();
    }
#endif

}
