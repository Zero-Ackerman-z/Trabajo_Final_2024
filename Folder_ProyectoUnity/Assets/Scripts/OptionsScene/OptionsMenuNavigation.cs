using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsMenuNavigation : ButtonNavigationBase
{
    public SettingsSelector settingsSelector;
    protected override void OnNavigate(InputAction.CallbackContext context)
    {
        if (isTransitioning) return;

        Vector2 navigationInput = context.ReadValue<Vector2>();
        float horizontalInput = navigationInput.x;
        float verticalInput = navigationInput.y;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            SelectButton(selectedIndex, false);

            if (horizontalInput != 0)
            {
                selectedIndex = (selectedIndex + (horizontalInput > 0 ? 1 : -1) + buttons.Length) % buttons.Length;
            }
            else if (verticalInput != 0)
            {
                selectedIndex = (selectedIndex + (verticalInput > 0 ? -1 : 1) + buttons.Length) % buttons.Length;
            }

            SelectButton(selectedIndex, true);
        }

        AudioManager.Instance?.PlayNavigateSFX();

    }
    protected override void OnSelect(InputAction.CallbackContext context)
    {
        if (isTransitioning) return;
        AudioManager.Instance?.PlaySelectSFX();

        if (selectedIndex == 0) // Botón de modo de pantalla izquierdo
        {
            settingsSelector.ChangeScreenMode(-1); // Cambiar modo de pantalla hacia la izquierda
        }
        else if (selectedIndex == 1) // Botón de dirección de flecha izquierda
        {
            settingsSelector.ChangeScreenMode(1); // Cambiar modo de pantalla hacia la izquierda
        }
        else if (selectedIndex == 2) // Botón de modo de juego izquierdo
        {
            settingsSelector.ChangeArrowDirection(-1); // Cambiar dirección de flecha hacia la derecha

        }
        else if (selectedIndex == 3) // Botón de modo de pantalla derecho
        {
            settingsSelector.ChangeArrowDirection(1); // Cambiar dirección de flecha hacia la derecha
        }
        else if (selectedIndex == 4) // Botón de dirección de flecha derecho
        {
            settingsSelector.ChangeGameMode(-1); // Cambiar modo de juego hacia la izquierda
        }
        else if (selectedIndex == 5) // Botón de modo de juego derecho
        {
            settingsSelector.ChangeGameMode(1); // Cambiar modo de juego hacia la izquierda
        }
    }

    protected override void OnBack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LoadScene("MainMenuScene");
        }
    }
}
