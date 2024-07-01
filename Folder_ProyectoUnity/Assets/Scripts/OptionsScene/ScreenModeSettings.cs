using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenModeSettings", menuName = "GameSettings/ScreenModeSettings", order = 3)]
public class ScreenModeSettings : ScriptableObject
{
    public int currentModeIndex = 0; 

    public string[] screenModes = { "Ventana Completa", "Ventana" };

    public void ChangeMode(int direction)
    {
        currentModeIndex = (currentModeIndex + direction + screenModes.Length) % screenModes.Length;
    }

    public string GetCurrentMode()
    {
        return screenModes[currentModeIndex];
    }

    public void ApplyScreenMode()
    {
        if (currentModeIndex == 0) // Ventana Completa
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else // Ventana
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}