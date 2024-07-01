using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public Button enterButton;
    private void Awake()
    {
        InitializeUI();

    }
    private void InitializeUI()
    {
        if (enterButton != null)
        {
            enterButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.CompleteFlashEvent += ShowEnterButton;
    }

    private void OnDisable()
    {
        EventManager.CompleteFlashEvent -= ShowEnterButton;
    }

    private void ShowEnterButton()
    {
        if (enterButton != null)
        {
            enterButton.gameObject.SetActive(true);
            enterButton.interactable = true;
        }
    }
}

