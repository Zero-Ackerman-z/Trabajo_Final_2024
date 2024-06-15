using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManajerPantallaprincipal : MonoBehaviour
{
    public Button flashCompletionButton; // El bot�n que se activar� despu�s del flash
    public GameObject logo;
    private void Awake()
    {
        // Desactivar el bot�n al inicio
        if (flashCompletionButton != null)
        {
            flashCompletionButton.gameObject.SetActive(false);
        }
        if (logo != null)
        {
            logo.gameObject.SetActive(false); // Desactivar el logo
        }
    }

    // M�todo para activar el bot�n despu�s de la animaci�n del flash
    public void ActivateFlashCompletionButton()
    {
        if (flashCompletionButton != null)
        {
            flashCompletionButton.gameObject.SetActive(true);
            flashCompletionButton.interactable = true;
        }
        if (logo != null)
        {
            logo.gameObject.SetActive(true); // Activar el logo
        }
    }
}
