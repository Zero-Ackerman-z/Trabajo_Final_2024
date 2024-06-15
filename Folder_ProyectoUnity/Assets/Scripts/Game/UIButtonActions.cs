using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonActions : MonoBehaviour
{
    public static UIButtonActions Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void OnStoryMode()
    {
        Debug.Log("Story Mode button clicked!");
        SceneManager.LoadScene("StoryModeScenes");
    }
    public void OnFreePlay()
    {
        Debug.Log("Free Play button clicked!");
        SceneManager.LoadScene("FreePlayScene");
    }

    public void OnCredits()
    {
        Debug.Log("Credits button clicked!");
        SceneManager.LoadScene("CreditsScene");
    }
    public void OnOptions()
    {
        Debug.Log("Options button clicked!");
        SceneManager.LoadScene("OptionsScene");
    }
}

