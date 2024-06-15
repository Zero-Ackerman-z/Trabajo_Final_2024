using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuHandler : MonoBehaviour
{
    private bool flashTriggeredOnce = false;
    [SerializeField] private float transitionDuration = 1.0f; // Duraci�n de la transici�n

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && flashTriggeredOnce)
        {
            EventManager.StartFlashEffect();
            StartTransition(() => {
                // Llamar a la siguiente escena despu�s de la transici�n
                UIButtonActions.Instance.MainMenu();
            });
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
    private void StartTransition(System.Action onComplete = null)
    {
        // Implementaci�n de la animaci�n de transici�n con DOTween
        transform.DOMoveY(transform.position.y + 10f, transitionDuration).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                onComplete?.Invoke(); // Llamar al callback cuando la transici�n haya terminado
            });
    }
}
