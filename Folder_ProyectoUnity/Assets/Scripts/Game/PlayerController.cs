using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAcctionsControllers controls;
    public static PlayerController Instance { get; private set; }

    private bool inputEnabled = true; // Controla si los inputs est�n habilitados o no

    private void Awake()
    {
        controls = new InputAcctionsControllers();

        controls.Game.Pause.performed += ctx => Pausar(ctx);
        controls.Game.AWSD.performed += ctx => MoverWASD(ctx);
        controls.Game.Flechas.performed += ctx => MoverFlechas(ctx);
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
        // Habilita los inputs cuando el objeto est� activo
        if (inputEnabled)
            controls.Game.Enable();
    }

    private void OnDisable()
    {
        // Deshabilita los inputs cuando el objeto est� desactivado
        controls.Game.Disable();
    }

    private void Seleccionar(InputAction.CallbackContext context)
    {
        if (inputEnabled)
        {

            string buttonName = gameObject.name;

            // Determinar la escena a cargar basada en el bot�n seleccionado
            switch (buttonName)
            {
                case "StartButton":
                    UIButtonActions.Instance.MainMenu();                    
                    break;
                case "OptionsButton":
                    UIButtonActions.Instance.OnStoryMode();
                    break;
                case "CreditsButton":
                    UIButtonActions.Instance.OnFreePlay();
                    break;
                // Agrega m�s casos seg�n sea necesario para otros botones y escenas
                default:
                    Debug.LogWarning("Bot�n no reconocido: " + buttonName);
                    break;
            }
        }
    }
    private void navigate(InputAction.CallbackContext context)
    {
        // L�gica para la acci�n de pausar
        if (inputEnabled)
        {
            // Solo si los inputs est�n habilitados
            // ...
        }
    }
    private void Salir(InputAction.CallbackContext context)
    {
        // L�gica para la acci�n de pausar
        if (inputEnabled)
        {
            // Solo si los inputs est�n habilitados
            // ...
        }
    }
    private void Pausar(InputAction.CallbackContext context)
    {
        // L�gica para la acci�n de pausar
        if (inputEnabled)
        {
            // Solo si los inputs est�n habilitados
            // ...
        }
    }

    private void MoverWASD(InputAction.CallbackContext context)
    {
        // L�gica para el movimiento con WASD
        if (inputEnabled)
        {
            // Solo si los inputs est�n habilitados
            // ...
        }
    }

    private void MoverFlechas(InputAction.CallbackContext context)
    {
        // L�gica para el movimiento con las flechas
        if (inputEnabled)
        {
            // Solo si los inputs est�n habilitados
            // ...
        }
    }

    private void MoverSDFJK(InputAction.CallbackContext context)
    {
        // L�gica para el movimiento con SDFJK
        if (inputEnabled)
        {
            // Solo si los inputs est�n habilitados
            // ...
        }
    }

    // M�todo para habilitar los inputs
    public void EnableInputs()
    {
        inputEnabled = true;
        controls.Game.Enable();
    }

    // M�todo para deshabilitar los inputs
    public void DisableInputs()
    {
        inputEnabled = false;
        controls.Game.Disable();
    }
}
