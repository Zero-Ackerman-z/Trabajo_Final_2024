using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static Action onFlash;
    
    // M�todo para iniciar el efecto de flash
    public static void StartFlashEffect()
    {
        onFlash?.Invoke();
    }
}
