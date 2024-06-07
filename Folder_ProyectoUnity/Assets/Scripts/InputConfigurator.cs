using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputConfigurator : MonoBehaviour
{
    public Dropdown inputDropdown;
    public PlayerController playerController;

    private void Start()
    {
        // Asignar el m�todo de cambio de configuraci�n al evento de cambio del Dropdown
        inputDropdown.onValueChanged.AddListener(delegate { ChangeInputConfig(); });

        // Cargar la configuraci�n actual del jugador
        LoadPlayerInputConfig();
    }

    // M�todo para cambiar la configuraci�n de entrada
    public void ChangeInputConfig()
    {
        string selectedOption = inputDropdown.options[inputDropdown.value].text;

        // Aplicar la configuraci�n de entrada seleccionada al PlayerController
        switch (selectedOption)
        {
            case "WASD":
                playerController.SetInputConfig(PlayerActions.WASD);
                break;
            case "DFJK":
                playerController.SetInputConfig(PlayerActions.DFJK);
                break;
            case "Arrow Keys":
                playerController.SetInputConfig(PlayerActions.ArrowKeys);
                break;
            default:
                break;
        }

        // Guardar la configuraci�n del jugador
        SavePlayerInputConfig(selectedOption);
    }

    // M�todo para cargar la configuraci�n de entrada del jugador
    private void LoadPlayerInputConfig()
    {
        // Aqu� puedes cargar la configuraci�n guardada previamente y configurar el Dropdown
    }

    // M�todo para guardar la configuraci�n de entrada del jugador
    private void SavePlayerInputConfig(string inputConfig)
    {
        // Aqu� puedes guardar la configuraci�n seleccionada por el jugador
    }
}
