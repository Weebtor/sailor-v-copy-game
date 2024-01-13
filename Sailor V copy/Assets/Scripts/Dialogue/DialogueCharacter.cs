using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Character")]
public class DialogueCharacter : ScriptableObject
{
    public string displayName;
    public Sprite sprite;
}
