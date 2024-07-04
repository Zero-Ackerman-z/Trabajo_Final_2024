using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class BaseDialogueManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI dialogueText;                       // Texto UI para mostrar el diálogo
    [SerializeField] protected Image backgroundImage;                   // Imagen de fondo UI
    [SerializeField] protected GameObject dialoguePanel;                // Panel de diálogo
    [SerializeField] protected AudioClip letterSound;                   // Sonido para cada letra

    protected bool isTyping = false;                                    // Indicador si está escribiendo texto
    protected Coroutine typingCoroutine;

    public abstract void StartDialogue();

    protected IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        char[] letters = text.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            char letter = letters[i];
            dialogueText.text += letter;
            if (letterSound != null)
            {
                AudioManager.Instance.PlayLetterSound();
            }
            yield return new WaitForSeconds(0.04f); // Tiempo entre cada letra
        }
        isTyping = false;

        OnTextDisplayed();
    }

    protected virtual void OnTextDisplayed() { }

    protected void ChangeBackground(Sprite newBackground)
    {
        if (newBackground != null)
        {
            backgroundImage.sprite = newBackground;
        }
    }

    protected void ChangeMusic(AudioClip newMusic)
    {
        if (newMusic != null)
        {
            AudioManager.Instance.PlayMusic(newMusic);
        }
    }
    protected void FadeOutText(float duration, System.Action onComplete)
    {
        dialogueText.CrossFadeAlpha(0, duration, false);
        StartCoroutine(DelayedCall(duration, () =>
        {
            // Después de desvanecerse, reiniciar la visualización del texto
            dialogueText.CrossFadeAlpha(1, 0, false); // Esto asegura que el texto vuelva a ser visible inmediatamente

            onComplete?.Invoke();
        }));
    }


    protected void FadeInText(float duration)
    {
        dialogueText.CrossFadeAlpha(1, duration, false);
        // Lógica adicional si es necesario después de desvanecerse
    }

    private IEnumerator DelayedCall(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }


}
