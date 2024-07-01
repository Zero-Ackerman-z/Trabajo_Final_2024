using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutomaticDialogueData", menuName = "ScriptableObjects/AutomaticDialogueData", order = 1)]
public class AutomaticDialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueEntry
    {
        public string dialogueName; // Nombre del di�logo, por ejemplo, "Intro", "Scene1", etc.

        [TextArea(3, 10)]
        public string text; // Texto del di�logo con m�s espacio en el Inspector
        public Sprite backgroundImage;        // Imagen de fondo asociada
        public bool changeMusic = false;      // Indica si se debe cambiar la m�sica
        public AudioClip newMusic;            // Nueva m�sica si `changeMusic` es verdadero
    }

    public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();
}
