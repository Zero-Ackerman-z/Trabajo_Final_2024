using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplashScreenSceneManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform logo;
    public Transform Fondo;

    public RectTransform[] uiElements;

    [Header("ScriptableObjects")]
    public DirectionSettings directionSettings;
    public GameModeSettings gameModeSettings;
    public ScreenModeSettings screenModeSettings;

    private bool flashTriggeredOnce = false;
    private bool enterPressed = false;

    private void Start()
    {
        ApplyInitialSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && flashTriggeredOnce && !enterPressed)
        {
            enterPressed = true;
            AudioManager.Instance?.PlaySelectSFX();
            StartCoroutine(ExecuteTransition());
        }
    }

    private void OnEnable()
    {
        EventManager.CompleteFlashEvent += OnFlashComplete;
    }

    private void OnDisable()
    {
        EventManager.CompleteFlashEvent -= OnFlashComplete;
    }

    private void OnFlashComplete()
    {
        flashTriggeredOnce = true;
    }

    private IEnumerator ExecuteTransition()
    {
        EventManager.StartFlashEffect();
        yield return new WaitForSeconds(1.5f);
        PlayTransition();
        yield return new WaitForSeconds(1.5f);
        UIButtonActions.Instance.MainMenu();
    }

    private void PlayTransition()
    {
        if (logo != null)
        {
            logo.DOMoveY(logo.position.y - Screen.height, 1.5f).SetEase(Ease.InOutQuad);
        }
        if (Fondo != null)
        {
            logo.DOMoveY(logo.position.y - Screen.height, 1.5f).SetEase(Ease.InOutQuad);
        }
        for (int i = 0; i < uiElements.Length; i++)
        {
            RectTransform uiElement = uiElements[i];
            if (uiElement != null && uiElement.gameObject.activeSelf)
            {
                uiElement.DOMoveY(uiElement.position.y - Screen.height, 1.5f).SetEase(Ease.InOutQuad);
            }
        }
    }

    private void ApplyInitialSettings()
    {
        ApplyDirectionSettings();
        ApplyGameModeSettings();
        ApplyScreenModeSettings();
    }

    private void ApplyDirectionSettings()
    {
        if (directionSettings != null)
        {
            string currentDirection = directionSettings.GetCurrentDirection();
            Debug.Log($"Configuración de dirección: {currentDirection}");
            float rotationAngle = currentDirection == "Up-Scroll" ? 0 : 180;
            logo.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }

    private void ApplyGameModeSettings()
    {
        if (gameModeSettings != null)
        {
            string currentMode = gameModeSettings.GetCurrentMode();
            Debug.Log($"Modo de juego: {currentMode}");
        }
    }

    private void ApplyScreenModeSettings()
    {
        if (screenModeSettings != null)
        {
            screenModeSettings.ApplyScreenMode();
            Debug.Log($"Modo de pantalla: {screenModeSettings.GetCurrentMode()}");
        }
    }
}