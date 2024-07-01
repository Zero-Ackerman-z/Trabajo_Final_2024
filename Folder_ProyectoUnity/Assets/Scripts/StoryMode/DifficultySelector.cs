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

        // Actualizar la visualización de la dificultad
        UpdateDifficultyDisplay();
    }

    private void OnDisable()
    {
        // Quitar los listeners cuando el objeto se desactiva
        difficultyLeftButton.onClick.RemoveAllListeners();
        difficultyRightButton.onClick.RemoveAllListeners();
    }

    // Método para cambiar la dificultad
    public void ChangeDifficulty(int direction)
    {
        // Cambiar el índice de la dificultad
        currentIndex = (currentIndex + direction + allConfigs.Length) % allConfigs.Length;
        UpdateDifficultyDisplay();
    }

    // Método para actualizar la visualización de la dificultad
    private void UpdateDifficultyDisplay()
    {
        difficultyDisplay.text = allConfigs[currentIndex].modeName;
    }

    // Método para obtener la configuración actual
    public GameModeConfig GetCurrentConfig()
    {
        return allConfigs[currentIndex];
    }
}