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
    public void PauseGame()
    {
        Time.timeScale = 0f; // Establece la escala de tiempo a 0 para pausar el juego
        Debug.Log("Juego pausado.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Restablece la escala de tiempo a 1 para reanudar el juego
        Debug.Log("Juego reanudado.");
    }

}

