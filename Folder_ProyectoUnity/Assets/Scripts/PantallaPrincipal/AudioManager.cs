using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource backgroundMusic;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip selectSFX;
    public AudioClip navigateSFX;
    public AudioClip letterSound; 

    [Header("Settings")]
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void PlaySelectSFX()
    {
        if (selectSFX != null)
        {
            sfxSource.PlayOneShot(selectSFX);
        }
    }

    public void PlayNavigateSFX()
    {
        if (navigateSFX != null)
        {
            sfxSource.PlayOneShot(navigateSFX);
        }
    }
    public void PlayLetterSound()
    {
        if (letterSound != null)
        {
            sfxSource.PlayOneShot(letterSound);
        }
    }
    public void PlayMusic(AudioClip clip)
    {
        if (backgroundMusic.clip == clip) return;
        backgroundMusic.DOFade(0, fadeDuration).OnComplete(() =>
        {
            backgroundMusic.clip = clip;
            backgroundMusic.Play();
            backgroundMusic.DOFade(1, fadeDuration);
        });
    }
}
