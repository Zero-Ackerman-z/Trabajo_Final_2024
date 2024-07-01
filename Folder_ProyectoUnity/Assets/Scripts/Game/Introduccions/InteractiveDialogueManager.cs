using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InteractiveDialogueManager : BaseDialogueManager
{
    [SerializeField] private InteractiveDialogueData interactiveDialogueData; // Datos del diálogo

    [SerializeField] private Image bfSpriteImage; // Imagen para BF
    [SerializeField] private Image gvSpriteImage; // Imagen para GV

    [SerializeField] private GameObject bfPanel; // Panel para el diálogo de BF
    [SerializeField] private GameObject gvPanel; // Panel para el diálogo de GV

    [SerializeField] private TextMeshProUGUI bfDialogueText; // Texto del diálogo para BF
    [SerializeField] private TextMeshProUGUI gvDialogueText; // Texto del diálogo para GV

    [SerializeField] private GameObject interactButton; // Botón para avanzar el diálogo

    private int currentDialogueIndex = 0;
    private InputAction advanceDialogueAction;
    private InputAction accelerateTypingAction;

    private void OnEnable()
    {
        var playerInput = new PlayerInput();
        advanceDialogueAction = playerInput.actions["AdvanceDialogue"];
        accelerateTypingAction = playerInput.actions["AccelerateTyping"];

        advanceDialogueAction.performed += ctx => AdvanceDialogue();
        accelerateTypingAction.started += ctx => SpeedUpTyping(true);
        accelerateTypingAction.canceled += ctx => SpeedUpTyping(false);
    }
    private void OnDisable()
    {
        advanceDialogueAction.performed -= ctx => AdvanceDialogue();
        accelerateTypingAction.started -= ctx => SpeedUpTyping(true);
        accelerateTypingAction.canceled -= ctx => SpeedUpTyping(false);
    }

    public override void StartDialogue()
    {
        currentDialogueIndex = 0; // Reiniciar el índice al inicio del diálogo
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (currentDialogueIndex >= interactiveDialogueData.dialogueEntries.Count)
        {
            EndDialogue();
            return;
        }

        var dialogueEntry = interactiveDialogueData.dialogueEntries[currentDialogueIndex];

        // Actualiza la imagen y el texto del personaje correspondiente
        if (dialogueEntry.isBF)
        {
            bfSpriteImage.sprite = dialogueEntry.characterSprite;
            bfDialogueText.text = ""; // Limpiar el texto anterior
            bfPanel.SetActive(true);
            gvPanel.SetActive(false);

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(dialogueEntry.text, 0.05f, bfDialogueText));
        }
        else
        {
            gvSpriteImage.sprite = dialogueEntry.characterSprite;
            gvDialogueText.text = ""; // Limpiar el texto anterior
            gvPanel.SetActive(true);
            bfPanel.SetActive(false);

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(dialogueEntry.text, 0.05f, gvDialogueText));
        }

        if (dialogueEntry.changeMusic)
        {
            ChangeMusic(dialogueEntry.newMusic);
        }
    }

    protected override void OnTextDisplayed()
    {
        interactButton.SetActive(true); // Mostrar el botón para avanzar el diálogo
    }

    private void AdvanceDialogue()
    {
        if (isTyping) return;

        interactButton.SetActive(false); // Ocultar el botón para avanzar el diálogo
        var dialogueEntry = interactiveDialogueData.dialogueEntries[currentDialogueIndex];

        currentDialogueIndex++;

        // Mueve el panel fuera de la pantalla y muestra el siguiente diálogo
        if (dialogueEntry.isBF)
        {
            bfPanel.transform.DOLocalMoveX(-2000, 0.5f).OnComplete(() =>
            {
                ShowDialogue();
                bfPanel.transform.DOLocalMoveX(0, 0.5f);
            });
        }
        else
        {
            gvPanel.transform.DOLocalMoveX(2000, 0.5f).OnComplete(() =>
            {
                ShowDialogue();
                gvPanel.transform.DOLocalMoveX(0, 0.5f);
            });
        }
    }

    private void SpeedUpTyping(bool speedUp)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        float typingSpeed = speedUp ? 0.01f : 0.05f;
        if (interactiveDialogueData.dialogueEntries.Count > currentDialogueIndex)
        {
            var currentText = interactiveDialogueData.dialogueEntries[currentDialogueIndex].text;
            var currentTextComponent = interactiveDialogueData.dialogueEntries[currentDialogueIndex].isBF ? bfDialogueText : gvDialogueText;
            typingCoroutine = StartCoroutine(TypeText(currentText, typingSpeed, currentTextComponent));
        }
    }

    private IEnumerator TypeText(string text, float typingSpeed, TextMeshProUGUI textComponent)
    {
        isTyping = true;
        textComponent.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            char letter = text[i];
            textComponent.text += letter; 

            if (letterSound != null)
            {
                AudioManager.Instance.PlayLetterSound(); 
            }

            yield return new WaitForSeconds(typingSpeed); 
        }

        isTyping = false;
        OnTextDisplayed();
    }

    private void EndDialogue()
    {
        Debug.Log("Diálogo interactivo completado");
        bfPanel.SetActive(false);
        gvPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }
}
