using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashEffect : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.1f;
    public float fadeDuration = 1.0f;

    private void OnEnable()
    {
        flashImage.enabled = false; // Desactivar la imagen al inicio
        EventManager.onFlash += StartFlashEffect;
    }

    private void OnDisable()
    {
        EventManager.onFlash -= StartFlashEffect;
    }

    private void StartFlashEffect()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        flashImage.enabled = true; // Activar la imagen al iniciar el flash

        float startTime = Time.time;

        // Flash blanco
        while (Time.time < startTime + flashDuration)
        {
            flashImage.color = Color.white;
            yield return null;
        }

        // Desvanecimiento
        float endTime = startTime + flashDuration + fadeDuration;
        while (Time.time < endTime)
        {
            float alpha = 1 - (Time.time - (startTime + flashDuration)) / fadeDuration;
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        flashImage.enabled = false; // Desactivar la imagen al finalizar el flash
    }
}