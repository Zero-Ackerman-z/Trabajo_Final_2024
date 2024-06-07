using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputConfigurator : MonoBehaviour
{
    public Dropdown inputDropdown;
    public PlayerController playerController;

    private void Start()
    {
        // Asignar el método de cambio de configuración al evento de cambio del Dropdown
        inputDropdown.onValueChanged.AddListener(delegate { ChangeInputConfig(); });

        // Cargar la configuración actual del jugador
        LoadPlayerInputConfig();
    }

    // Método para cambiar la configuración de entrada
    public void ChangeInputConfig()
    {
        string selectedOption = inputDropdown.options[inputDropdown.value].text;

        // Aplicar la configuración de entrada seleccionada al PlayerController
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

        // Guardar la configuración del jugador
        SavePlayerInputConfig(selectedOption);
    }

    // Método para cargar la configuración de entrada del jugador
    private void LoadPlayerInputConfig()
    {
        // Aquí puedes cargar la configuración guardada previamente y configurar el Dropdown
    }

    // Método para guardar la configuración de entrada del jugador
    private void SavePlayerInputConfig(string inputConfig)
    {
        // Aquí puedes guardar la configuración seleccionada por el jugador
    }
}
