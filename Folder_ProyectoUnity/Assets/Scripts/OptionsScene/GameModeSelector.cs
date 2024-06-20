using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameModeSelector : MonoBehaviour
{
    public TextMeshProUGUI modeText;
    public string[] modes = { "Flechas", "DFJK", "AWSD" };
    private int currentModeIndex = 0;
    public Button leftButton;
    public Button rightButton;

    private void Start()
    {
        currentModeIndex = PlayerPrefs.GetInt("GameMode", 0);
        UpdateModeText();
    }

    private void OnEnable()
    {
        leftButton.onClick.AddListener(ChangeModeLeft);
        rightButton.onClick.AddListener(ChangeModeRight);
    }

    private void OnDisable()
    {
        leftButton.onClick.RemoveListener(ChangeModeLeft);
        rightButton.onClick.RemoveListener(ChangeModeRight);
    }

    public void ChangeModeLeft()
    {
        currentModeIndex = (currentModeIndex - 1 + modes.Length) % modes.Length;
        UpdateModeText();
        AudioManager.Instance?.PlaySelectSFX();
        SaveMode();
    }

    public void ChangeModeRight()
    {
        currentModeIndex = (currentModeIndex + 1) % modes.Length;
        UpdateModeText();
        AudioManager.Instance?.PlaySelectSFX();
        SaveMode();
    }

    private void UpdateModeText()
    {
        modeText.text = modes[currentModeIndex];
    }

    private void SaveMode()
    {
        PlayerPrefs.SetInt("GameMode", currentModeIndex);
        PlayerPrefs.Save();
    }

    
}
