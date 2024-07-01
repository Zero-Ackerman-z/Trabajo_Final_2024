using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GameModeSettings", menuName = "GameSettings/GameModeSettings", order = 2)]
public class GameModeSettings : ScriptableObject
{
    [System.Serializable]
    public class ModeSettings
    {
        public string modeName;
        public InputActionReference[] actions;
    }

    public int currentModeIndex = 0;
    public ModeSettings[] modes;

    // Cambiar el modo de juego
    public void ChangeMode(int direction)
    {
        currentModeIndex = (currentModeIndex + direction + modes.Length) % modes.Length;
    }

    // Obtener el nombre del modo actual
    public string GetCurrentMode()
    {
        if (modes == null || modes.Length == 0) return "No Mode Available";

        return modes[currentModeIndex].modeName;
    }

    // Obtener las acciones del modo actual
    public InputActionReference[] GetCurrentModeActions()
    {
        if (modes == null || modes.Length == 0) return null;

        return modes[currentModeIndex].actions;
    }
}