using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    [Header("Difficulty Settings")]
    public Button difficultyLeftButton;
    public Button difficultyRightButton;
    public TextMeshProUGUI difficultyDisplay;

    public GameModeConfig easyModeConfig;
    public GameModeConfig normalModeConfig;
    public GameModeConfig hardModeConfig;

    private GameModeConfig[] allConfigs;
    private int currentIndex = 0;

    private void OnEnable()
    {
        // Configurar los botones de izquierda y derecha
        difficultyLeftButton.onClick.AddListener(() => ChangeDifficulty(-1));
        difficultyRightButton.onClick.AddListener(() => ChangeDifficulty(1));

        // Inicializar el array de configuraciones de dificultad
        allConfigs = new[] { easyModeConfig, normalModeConfig, hardModeConfig };

        // Actualizar la visualizaci�n de la dificultad
        UpdateDifficultyDisplay();
    }

    private void OnDisable()
    {
        // Quitar los listeners cuando el objeto se desactiva
        difficultyLeftButton.onClick.RemoveAllListeners();
        difficultyRightButton.onClick.RemoveAllListeners();
    }

    // M�todo para cambiar la dificultad
    public void ChangeDifficulty(int direction)
    {
        // Cambiar el �ndice de la dificultad
        currentIndex = (currentIndex + direction + allConfigs.Length) % allConfigs.Length;
        UpdateDifficultyDisplay();
    }

    // M�todo para actualizar la visualizaci�n de la dificultad
    private void UpdateDifficultyDisplay()
    {
        difficultyDisplay.text = allConfigs[currentIndex].modeName;
    }

    // M�todo para obtener la configuraci�n actual
    public GameModeConfig GetCurrentConfig()
    {
        return allConfigs[currentIndex];
    }
}