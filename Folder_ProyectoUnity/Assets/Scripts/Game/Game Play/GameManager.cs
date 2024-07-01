using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public AutomaticDialogueManager automaticDialogueManager; // Referencia al manejador de di�logo autom�tico

    private void Start()
    {
        if (automaticDialogueManager != null)
        {
            automaticDialogueManager.StartDialogue();
        }
        else
        {
            Debug.LogError("AutomaticDialogueManager no est� asignado en el GameManager.");
        }
    }
}

