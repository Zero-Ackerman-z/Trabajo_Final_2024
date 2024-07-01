using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; // Importar DOTween
public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia al Text de la puntuación
    public TextMeshProUGUI messageText; // Referencia al Text de la puntuación
    public ComboTextPool comboTextPool; // Usamos el pool en lugar del prefab directamente
    public Transform comboTextParent; // Asegúrate de asignar esto desde el Inspector
    public Vector3 comboTextStartPosition; // Posición inicial fija para los textos de combo


    private int score;
    private int comboCount;

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText reference is not set in GameController!");
        }
        if (messageText == null)
        {
            Debug.LogError("messageText reference is not set in GameController!");
        }
        if (comboTextParent == null)
        {
            Debug.LogError("comboTextParent reference is not set in GameController!");
        }
        else
        {
            score = 0;
            comboCount = 0;
            UpdateScoreText();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
    public void IncreaseCombo()
    {
        comboCount++;
        ShowComboText();

    }
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.alpha = 1f; // Asegurarse de que el alpha sea 1 al mostrar el texto

        // Tween para desvanecer el texto después de unos segundos
        messageText.DOFade(0f, 0.2f).SetDelay(0.7f).OnComplete(() =>
        {
            messageText.text = ""; // Limpiar el texto después del desvanecimiento
        });
    }
    public void ResetCombo()
    {
        comboCount = 0;
    }
    private void UpdateScoreText()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText reference is not set in GameController!");
            return;
        }

        scoreText.text = "Score:   " + score;
    }
    private void ShowComboText()
    {
        if (comboTextPool == null || comboTextParent == null)
        {
            Debug.LogError("comboTextPool or comboTextParent is not set in GameController!");
            return;
        }

        TextMeshProUGUI comboTextInstance = comboTextPool.GetComboText();
        comboTextInstance.text = "Combo:" + comboCount;

        // Establecer la posición inicial fija para los textos de combo
        comboTextInstance.transform.SetParent(comboTextParent, false);
        comboTextInstance.transform.localPosition = comboTextStartPosition;

        // Establecer la opacidad inicial
        Color textColor = comboTextInstance.color;
        textColor.a = 0.2f; // Opacidad inicial mientras baja
        comboTextInstance.color = textColor;

        // Definir las posiciones finales de la animación
        Vector3 upPosition = comboTextStartPosition + new Vector3(0, 40f, 0); // Posición hacia arriba
        Vector3 downPosition = comboTextStartPosition; // Retorna a la posición inicial

        // Animar el texto de combo
        comboTextInstance.DOFade(1f, 0f); // Asegura que el texto esté visible
        comboTextInstance.transform.DOLocalMove(upPosition, 0.1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                comboTextInstance.transform.DOLocalMove(downPosition, 0.1f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        comboTextInstance.DOFade(0f, 0.1f)
                            .SetDelay(0.1f)
                            .OnComplete(() =>
                            {
                                comboTextPool.ReturnComboText(comboTextInstance);
                            });
                    });
            });
    }
}