using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    public Toggle fullscreenToggle; // Referencia al Toggle UI que controla pantalla completa

    private void Start()
    {
        // Establecer el estado del Toggle según la preferencia guardada
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen; // Cambiar entre pantalla completa y ventana

        // Guardar la preferencia de pantalla completa en PlayerPrefs
        PlayerPrefs.SetInt("FullScreenMode", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}