using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    public DialogueTableObject dialogueTable;
    public TextMeshProUGUI dialogueTextUI;
    public float letterDelay = 0.05f; // Delay entre letras
    private string currentDialogue; // Diálogo actual
    private int currentIndex; // Índice de letra actual
    private bool displayingDialogue; // Indica si se está mostrando el diálogo

    private void Start()
    {
        DisplayDialogue(0);
    }

    public void DisplayDialogue(int index)
    {
        if (index < 0 || index >= dialogueTable.dialogueEntries.Length)
        {
            Debug.LogWarning("Invalid dialogue index: " + index);
            return;
        }

        currentDialogue = dialogueTable.dialogueEntries[index].dialogueText;
        currentIndex = 0;
        displayingDialogue = true; // Se inicia la animación
        StartCoroutine(AnimateText()); // Se inicia la animación de texto
    }

    IEnumerator AnimateText()
    {
        dialogueTextUI.text = ""; // Se inicia con un texto vacío

        // Mientras haya letras por mostrar y la animación esté activa
        while (currentIndex < currentDialogue.Length && displayingDialogue)
        {
            // Se añade una letra al texto
            dialogueTextUI.text += currentDialogue[currentIndex];
            currentIndex++;

            // Se espera un tiempo antes de mostrar la siguiente letra
            yield return new WaitForSeconds(letterDelay);
        }

        displayingDialogue = false; // Se desactiva la animación al finalizar
    }
}
