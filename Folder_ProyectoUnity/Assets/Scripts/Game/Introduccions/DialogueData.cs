using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class InteractiveDialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueEntry
    {
        public string dialogueName; // Nombre del diálogo, por ejemplo, "Intro", "Scene1", etc.

        [TextArea(3, 10)]
        public string text; // Texto del diálogo con más espacio en el Inspector
        public Sprite characterSprite;        // Sprite del personaje hablando
        public bool isBF;                     // True si es BF, false si es GV
        public bool changeMusic = false;      // Indica si se debe cambiar la música
        public AudioClip newMusic;            // Nueva música si `changeMusic` es verdadero
    }

    public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();
}