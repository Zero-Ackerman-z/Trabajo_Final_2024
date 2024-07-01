using System.Collections;
using UnityEngine;

public class AutomaticDialogueManager : BaseDialogueManager
{
    [SerializeField] private AutomaticDialogueData automaticDialogueData; // El ScriptableObject con los datos del diálogo
    [SerializeField] private float textDisplayDuration = 2f;              // Duración antes de que el texto desaparezca

    private int currentDialogueIndex = 0;

    public override void StartDialogue()
    {
        dialoguePanel.SetActive(true); // Asegurar que el panel de diálogo esté activo al iniciar
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        Debug.Log("Mostrando diálogo número: " + currentDialogueIndex);

        if (currentDialogueIndex >= automaticDialogueData.dialogueEntries.Count)
        {
            EndDialogue();
            return;
        }

        var dialogueEntry = automaticDialogueData.dialogueEntries[currentDialogueIndex];

        ChangeBackground(dialogueEntry.backgroundImage);

        if (dialogueEntry.changeMusic)
        {
            ChangeMusic(dialogueEntry.newMusic);
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(dialogueEntry.text));
    }

    protected override void OnTextDisplayed()
    {
        StartCoroutine(WaitAndFadeOut());
    }

    private IEnumerator WaitAndFadeOut()
    {
        yield return new WaitForSeconds(textDisplayDuration);

        FadeOutText(1f, () =>
        {
            ResetDialogueVisuals(); // Restablecer los elementos visuales del diálogo

            currentDialogueIndex++; // Incrementar el índice para avanzar al siguiente diálogo

            // Verificar si aún hay más diálogos por mostrar
            if (currentDialogueIndex < automaticDialogueData.dialogueEntries.Count)
            {
                ShowDialogue();
            }
            else
            {
                EndDialogue(); // Llamar a EndDialogue() si hemos mostrado todos los diálogos
            }
        });
    }

    private void ResetDialogueVisuals()
    {
        dialogueText.text = ""; // Reiniciar el texto del diálogo
        dialogueText.alpha = 1f; // Asegurarse de que el texto esté completamente visible
                                 // Restablecer la imagen de fondo u otros elementos visuales si es necesario
        ChangeBackground(null); // Por ejemplo, cambiar a una imagen de fondo nula o predeterminada
    }


    private void EndDialogue()
    {
        Debug.Log("Diálogo automático completado");
        dialoguePanel.SetActive(false);
        EventManager.StartCountdownEvent?.Invoke(); // Invocar el nuevo evento para iniciar el conteo regresivo


    }
}