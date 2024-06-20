using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    public Button enterButton;

    private void Awake()
    {
        if (enterButton != null)
        {
            enterButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.onFlashComplete += ShowEnterButton;
    }

    private void OnDisable()
    {
        EventManager.onFlashComplete -= ShowEnterButton;
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

