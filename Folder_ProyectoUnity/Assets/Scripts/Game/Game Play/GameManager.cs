using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public AutomaticDialogueManager automaticDialogueManager; // Referencia al manejador de diálogo automático

    private void Start()
    {
        if (automaticDialogueManager != null)
        {
            automaticDialogueManager.StartDialogue();
        }
        else
        {
            Debug.LogError("AutomaticDialogueManager no está asignado en el GameManager.");
        }
    }
}

