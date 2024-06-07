using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAcctionsControllers controls;
    public static PlayerController Instance { get; private set; }

    private bool inputEnabled = true; // Controla si los inputs están habilitados o no

    private void Awake()
    {
        controls = new InputAcctionsControllers();

        // Asigna las funciones de callback a los eventos de input
        controls.Acciones.Seleccionar.performed += ctx => Seleccionar(ctx);
        controls.Game.Pause.performed += ctx => Pausar(ctx);

        // WASD Controls
        controls.Game.AWSD.performed += ctx => MoverWASD(ctx);

        // Arrow Controls
        controls.Game.Flechas.performed += ctx => MoverFlechas(ctx);

        // SDFJK Controls
        controls.Game.DFJK.performed += ctx => MoverSDFJK(ctx);

        if (Instance == null)
        {
            // Si no hay ninguna instancia, establecer esta como la instancia Singleton
            Instance = this;
        }
        else
        {
            // Si ya hay una instancia, destruir este GameObject para evitar duplicados
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Habilita los inputs cuando el objeto está activo
        if (inputEnabled)
            controls.Game.Enable();
    }

    private void OnDisable()
    {
        // Deshabilita los inputs cuando el objeto está desactivado
        controls.Game.Disable();
    }

    private void Seleccionar(InputAction.CallbackContext context)
    {
        // Lógica para la acción de seleccionar
        if (inputEnabled)
        {
            // Solo si los inputs están habilitados
            // ...
        }
    }

    private void Pausar(InputAction.CallbackContext context)
    {
        // Lógica para la acción de pausar
        if (inputEnabled)
        {
            // Solo si los inputs están habilitados
            // ...
        }
    }

    private void MoverWASD(InputAction.CallbackContext context)
    {
        // Lógica para el movimiento con WASD
        if (inputEnabled)
        {
            // Solo si los inputs están habilitados
            // ...
        }
    }

    private void MoverFlechas(InputAction.CallbackContext context)
    {
        // Lógica para el movimiento con las flechas
        if (inputEnabled)
        {
            // Solo si los inputs están habilitados
            // ...
        }
    }

    private void MoverSDFJK(InputAction.CallbackContext context)
    {
        // Lógica para el movimiento con SDFJK
        if (inputEnabled)
        {
            // Solo si los inputs están habilitados
            // ...
        }
    }

    // Método para habilitar los inputs
    public void EnableInputs()
    {
        inputEnabled = true;
        controls.Game.Enable();
    }

    // Método para deshabilitar los inputs
    public void DisableInputs()
    {
        inputEnabled = false;
        controls.Game.Disable();
    }
}
