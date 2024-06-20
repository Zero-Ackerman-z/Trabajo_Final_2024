using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManajerPantallaprincipal : MonoBehaviour
{
    public Button flashCompletionButton; // El botón que se activará después del flash
    public GameObject logo;
    private void Awake()
    {
        // Desactivar el botón al inicio
        if (flashCompletionButton != null)
        {
            flashCompletionButton.gameObject.SetActive(false);
        }
        if (logo != null)
        {
            logo.gameObject.SetActive(false); // Desactivar el logo
        }
    }

    // Método para activar el botón después de la animación del flash
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
