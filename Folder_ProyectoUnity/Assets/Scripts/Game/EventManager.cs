using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action onFlash;
    public static Action onFlashComplete;

    // Método para iniciar el efecto de flash
    public static void StartFlashEffect()
    {
        onFlash?.Invoke();

    }
    public static void CompleteFlashEffect()
    {
        onFlashComplete?.Invoke();
        UIManajerPantallaprincipal uiManager = FindObjectOfType<UIManajerPantallaprincipal>();
        if (uiManager != null)
        {
            uiManager.ActivateFlashCompletionButton();
        }
    }
}
