using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueTableObject")]
public class DialogueTableObject : ScriptableObject
{
    [System.Serializable]
    public struct DialogueEntry
    {
        public string characterName;
        [TextArea(3, 10)]
        public string dialogueText;
    }

    public DialogueEntry[] dialogueEntries;
}