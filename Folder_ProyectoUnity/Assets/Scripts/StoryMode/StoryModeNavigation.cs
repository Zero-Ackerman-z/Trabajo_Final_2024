using UnityEngine;
using UnityEngine.InputSystem;
public class StoryModeNavigation : ButtonNavigationBase
{
    public DifficultySelector difficultySelector; // Para manejar la selección de dificultad 
    protected override void OnNavigate(InputAction.CallbackContext context)
    {
        if (isTransitioning) return;

        Vector2 navigationInput = context.ReadValue<Vector2>();
        float horizontalInput = navigationInput.x;

        if (horizontalInput != 0)
        {
            SelectButton(selectedIndex, false);
            selectedIndex = (selectedIndex + (horizontalInput > 0 ? 1 : -1) + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex, true);
        }

        AudioManager.Instance?.PlayNavigateSFX();
    }

    protected override void OnSelect(InputAction.CallbackContext context)
    {
        if (isTransitioning) return;

        AudioManager.Instance?.PlaySelectSFX();

        if (selectedIndex == 0) // Botón de dificultad izquierda
        {
            buttons[selectedIndex].onClick.Invoke(); // Invoca la acción del botón de iniciar la semana
        }
        else if (selectedIndex == 1) // Botón de semana
        {
            difficultySelector.ChangeDifficulty(-1); // Cambiar dificultad a la izquierda
        }
        else if (selectedIndex == 2) // Botón de dificultad izquierda
        {
            difficultySelector.ChangeDifficulty(1); // Cambiar dificultad a la derecha
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
