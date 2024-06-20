using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton
    public AudioSource backgroundMusic;
    public AudioSource sfx;
    public AudioClip selectSFX; // SFX para selecci�n
    public AudioClip navigateSFX; // SFX para navegaci�n
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir el objeto al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Destruir el objeto si ya existe una instancia
        }
    }

    private void Start()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play(); // Reproducir m�sica de fondo si no est� reproduci�ndose
        }
    }


    public void PlaySelectSFX()
    {
        if (selectSFX != null)
        {
            sfx.PlayOneShot(selectSFX); // Reproducir SFX de selecci�n
        }
    }

    public void PlayNavigateSFX()
    {
        if (navigateSFX != null)
        {
            sfx.PlayOneShot(navigateSFX); // Reproducir SFX de navegaci�n
        }
    }
}
