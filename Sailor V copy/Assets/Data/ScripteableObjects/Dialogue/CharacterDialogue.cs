using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Dialogue", menuName = "Dialogue/Character Dialogue")]
public class CharacterDialogue : ScriptableObject
{
    public string displayName;
    public Sprite sprite;
}
