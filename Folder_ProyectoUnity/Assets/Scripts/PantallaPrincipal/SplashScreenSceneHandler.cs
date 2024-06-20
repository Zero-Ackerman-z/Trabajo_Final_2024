using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SplashScreenSceneHandler : MonoBehaviour
{
    public Transform logo; // Referencia al logo que es un sprite
    public RectTransform[] uiElements; // Referencias a los elementos de la UI que se moverán
    private bool flashTriggeredOnce = false;
    private bool enterPressed = false; // Para evitar múltiples llamados

    private void Update()
    {
        // Solo reproducir el sonido y empezar la transición al presionar Enter
        if (Input.GetKeyDown(KeyCode.Return) && flashTriggeredOnce && !enterPressed)
        {
            enterPressed = true; // Asegurarse de que esto se ejecute una sola vez
            AudioManager.Instance?.PlaySelectSFX();
            StartCoroutine(ExecuteTransition());
        }
    }

    private void OnEnable()
    {
        EventManager.onFlashComplete += OnFlashComplete;
    }

    private void OnDisable()
    {
        EventManager.onFlashComplete -= OnFlashComplete;
    }

    private void OnFlashComplete()
    {
        flashTriggeredOnce = true;
    }
    private IEnumerator ExecuteTransition()
    {
        // Llama al flash inmediatamente al presionar Enter
        EventManager.StartFlashEffect();

        // Espera a que el flash termine (tiempo suficiente para el efecto de flash)
        yield return new WaitForSeconds(1.5f);

        // Iniciar la animación de transición de los elementos de la UI y el logo
        PlayTransition();

        // Esperar unos segundos para la animación de la transición
        yield return new WaitForSeconds(1.5f);

        // Cambiar a la siguiente escena
        SceneManager.LoadScene("MainMenuScene");
    }

    private void PlayTransition()
    {
        // Mover el logo hacia abajo fuera de la pantalla
        if (logo != null)
        {
            logo.DOMoveY(logo.position.y - Screen.height, 1.5f).SetEase(Ease.InOutQuad);
        }

        // Mover cada elemento de la UI hacia abajo fuera de la pantalla
        foreach (RectTransform uiElement in uiElements)
        {
            if (uiElement != null && uiElement.gameObject.activeSelf)
            {
                uiElement.DOMoveY(uiElement.position.y - Screen.height, 1.5f).SetEase(Ease.InOutQuad);
            }
        }
    }
}