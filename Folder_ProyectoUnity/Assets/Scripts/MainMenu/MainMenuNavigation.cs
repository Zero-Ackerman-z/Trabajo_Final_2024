using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuNavigation : ButtonNavigationBase
{
    protected override void OnNavigate(InputAction.CallbackContext context)
    {
        if (isTransitioning) return;

        Vector2 navigationInput = context.ReadValue<Vector2>();

        if (navigationInput.y != 0)
        {
            SelectButton(selectedIndex, false);

            // Navegación vertical (arriba/abajo)
            selectedIndex = (selectedIndex + (navigationInput.y > 0 ? -1 : 1) + buttons.Length) % buttons.Length;

            SelectButton(selectedIndex, true);
        }

        AudioManager.Instance?.PlayNavigateSFX();
    }

    protected override void OnSelect(InputAction.CallbackContext context)
    {
        if (isTransitioning) return;

        AudioManager.Instance?.PlaySelectSFX();

        HideOtherButtons();
        isTransitioning = true;

        transitionManager.StartBackgroundTransition(() =>
        {
            buttons[selectedIndex].onClick.Invoke();
            isTransitioning = false;
        });
    }

    protected override void OnBack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LoadScene("MainScreenScene");
        }
    }
}
