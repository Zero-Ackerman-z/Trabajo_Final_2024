using System.Collections;
using UnityEngine;

public class AutomaticDialogueManager : BaseDialogueManager
{
    [SerializeField] private AutomaticDialogueData automaticDialogueData; // El ScriptableObject con los datos del di�logo
    [SerializeField] private float textDisplayDuration = 2f;              // Duraci�n antes de que el texto desaparezca

    private int currentDialogueIndex = 0;

    public override void StartDialogue()
    {
        dialoguePanel.SetActive(true); // Asegurar que el panel de di�logo est� activo al iniciar
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        Debug.Log("Mostrando di�logo n�mero: " + currentDialogueIndex);

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
            ResetDialogueVisuals(); // Restablecer los elementos visuales del di�logo

            currentDialogueIndex++; // Incrementar el �ndice para avanzar al siguiente di�logo

            // Verificar si a�n hay m�s di�logos por mostrar
            if (currentDialogueIndex < automaticDialogueData.dialogueEntries.Count)
            {
                ShowDialogue();
            }
            else
            {
                EndDialogue(); // Llamar a EndDialogue() si hemos mostrado todos los di�logos
            }
        });
    }

    private void ResetDialogueVisuals()
    {
        dialogueText.text = ""; // Reiniciar el texto del di�logo
        dialogueText.alpha = 1f; // Asegurarse de que el texto est� completamente visible
                                 // Restablecer la imagen de fondo u otros elementos visuales si es necesario
        ChangeBackground(null); // Por ejemplo, cambiar a una imagen de fondo nula o predeterminada
    }


    private void EndDialogue()
    {
        Debug.Log("Di�logo autom�tico completado");
        dialoguePanel.SetActive(false);
        EventManager.StartCountdownEvent?.Invoke(); // Invocar el nuevo evento para iniciar el conteo regresivo


    }
}