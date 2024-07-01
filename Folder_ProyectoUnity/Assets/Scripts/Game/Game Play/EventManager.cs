using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action onFlash;
    public static Action CompleteFlashEvent;

    public static Action StartTransitionEvent;
    public static Action StartCountdownEvent; // Nuevo evento para iniciar el conteo regresivo
    public static Action CountdownCompletedEvent; // Nuevo evento para iniciar el conteo regresivo

    public static EventManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Método para iniciar el efecto de flash
    public static void StartFlashEffect()
    {
        onFlash?.Invoke();

    }

    // Método estático para iniciar la transición de pantalla
    public static void StartTransition()
    {
        StartTransitionEvent?.Invoke();
    }

    // Método estático para completar el efecto de flash
    public static void CompleteFlash()
    {
        CompleteFlashEvent?.Invoke();
    }
    public static void StartCountdown()
    {
        StartCountdownEvent?.Invoke();
    }
    public static void CountdownComplete()
    {
        CountdownCompletedEvent?.Invoke();
    }
    public static void WarningPanelOpened()
    {
    }
    public static void WarningPanelClosed()
    {
    }
}
