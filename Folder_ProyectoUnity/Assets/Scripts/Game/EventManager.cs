using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action onFlash;
    
    // Método para iniciar el efecto de flash
    public static void StartFlashEffect()
    {
        onFlash?.Invoke();
    }
}
