using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Image characterIcon;
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;
    // public TextMeshProUGUI dialogueText2;

    public GameObject dialogueBox;

    // DialogueObject currentDialogue;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void StartDialogue(DialogueTree dialogue)
    {
        // currentDialogue = dialogue;
        dialogueBox.SetActive(true);
        // UpdateDialogueBox(dialogue.startItem);
        // UpdateDialogueBox(dialogue.dialogueLines[0
        // foreach (DialogueData line in dialogue.dialogueLines)
        // {
        //     Debug.Log($"<color=green>[{line.character.displayName}]</color>: {line.dialogueText}");
        // }
    }

    void UpdateDialogueBox(DialogueItem d_item)
    {
        var item = (DialogueTextItem)d_item;
        Debug.Log(item.name);
        Debug.Log(item.text);
        // characterNameText.text = line.character.displayName;
        // dialogueText.text = line.dialogueText;
    }



}
