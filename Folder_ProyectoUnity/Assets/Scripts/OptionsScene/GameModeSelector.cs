using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsSelector : MonoBehaviour
{
    [Header("Screen Mode Settings")]
    public Button screenModeLeftButton;
    public Button screenModeRightButton;
    public TextMeshProUGUI screenModeDisplay;
    public ScreenModeSettings screenModeSettings;

    [Header("Arrow Direction Settings")]
    public Button arrowDirectionLeftButton;
    public Button arrowDirectionRightButton;
    public TextMeshProUGUI arrowDirectionDisplay;
    public DirectionSettings directionSettings;

    [Header("Game Mode Settings")]
    public TextMeshProUGUI gameModeDisplay;
    public Button gameModeLeftButton;
    public Button gameModeRightButton;
    public GameModeSettings gameModeSettings;

    private void OnEnable()
    {
        // Configurar los botones de izquierda y derecha
        screenModeLeftButton.onClick.AddListener(() => ChangeScreenMode(-1));
        screenModeRightButton.onClick.AddListener(() => ChangeScreenMode(1));

        arrowDirectionLeftButton.onClick.AddListener(() => ChangeArrowDirection(-1));
        arrowDirectionRightButton.onClick.AddListener(() => ChangeArrowDirection(1));

        gameModeLeftButton.onClick.AddListener(() => ChangeGameMode(-1));
        gameModeRightButton.onClick.AddListener(() => ChangeGameMode(1));

        UpdateDisplays();
    }

    // Método para cambiar el modo de pantalla
    public void ChangeScreenMode(int direction)
    {
        screenModeSettings.ChangeMode(direction);
        UpdateScreenModeDisplay();
    }

    // Método para cambiar la dirección de la flecha
    public void ChangeArrowDirection(int direction)
    {
        directionSettings.ChangeDirection(direction);
        //UpdateArrowDirectionDisplay();
    }

    // Método para cambiar el modo de juego
    public void ChangeGameMode(int direction)
    {
        gameModeSettings.ChangeMode(direction);
        UpdateGameModeDisplay();
    }

    // Actualiza los textos de pantalla, dirección y modo de juego
    private void UpdateDisplays()
    {
        UpdateScreenModeDisplay();
        //UpdateArrowDirectionDisplay();
        UpdateGameModeDisplay();
    }

    private void UpdateScreenModeDisplay()
    {
        screenModeDisplay.text = screenModeSettings.GetCurrentMode();
    }

   /* private void UpdateArrowDirectionDisplay()
    {
        arrowDirectionDisplay.text = directionSettings.GetCurrentDirection();
    }*/

    private void UpdateGameModeDisplay()
    {
        gameModeDisplay.text = gameModeSettings.GetCurrentMode();
    }
}
