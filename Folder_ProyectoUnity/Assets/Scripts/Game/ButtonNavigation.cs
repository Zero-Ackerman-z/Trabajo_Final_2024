using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonNavigation : MonoBehaviour
{
    public Button[] buttons;
    private int selectedIndex = 0;
    private InputAcctionsControllers inputActions;

    private void Awake()
    {
        inputActions = new InputAcctionsControllers();
    }

    private void OnEnable()
    {
        inputActions.UI.Navigate.performed += OnNavigate;
        inputActions.UI.Select.performed += OnSelect;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Navigate.performed -= OnNavigate;
        inputActions.UI.Select.performed -= OnSelect;
        inputActions.Disable();
    }

    private void Start()
    {
        SelectButton(selectedIndex);
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 navigationInput = context.ReadValue<Vector2>();
        float verticalInput = navigationInput.y;

        if (verticalInput != 0)
        {
            selectedIndex = (selectedIndex + (verticalInput > 0 ? -1 : 1) + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex);
        }
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        buttons[selectedIndex].onClick.Invoke();
    }

    // Método para seleccionar un botón y resaltar su estado
    private void SelectButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false; // Desactivar todos los botones
        }
        buttons[index].Select(); // Seleccionar el botón indicado
        buttons[index].interactable = true; // Activar el botón seleccionado
    }
}