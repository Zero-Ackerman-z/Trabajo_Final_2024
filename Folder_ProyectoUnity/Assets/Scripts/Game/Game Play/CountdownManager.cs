using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private AudioClip[] countdownSounds; // Cambiado a un array de clips de audio para cada cuenta regresiva y "Go!"
    [SerializeField] private float countdownDuration = 0.5f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource del GameObject si es necesario usarlo directamente
        EventManager.StartCountdownEvent += StartCountdownHandler;
    }

    private void StartCountdownHandler()
    {
        StartCoroutine(CountdownSequence());
    }

    private IEnumerator CountdownSequence()
    {
        string[] countdownTexts = { "3", "2", "1", "Go!" };

        for (int i = 0; i < countdownTexts.Length; i++)
        {
            countdownText.text = countdownTexts[i];

            if (i < countdownSounds.Length && countdownSounds[i] != null)
            {
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(countdownSounds[i]); // Reproducir el clip de audio usando PlayOneShot
                }
                else
                {
                    AudioSource.PlayClipAtPoint(countdownSounds[i], Camera.main.transform.position); // Si no se usa un AudioSource propio
                }
            }

            countdownText.CrossFadeAlpha(1, 0.5f, false);
            yield return new WaitForSeconds(countdownDuration);
            countdownText.CrossFadeAlpha(0, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }

        countdownText.text = "";
        EventManager.CountdownCompletedEvent?.Invoke();

    }

}
