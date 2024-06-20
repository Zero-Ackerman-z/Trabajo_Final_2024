using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSequence : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float wordInterval; // Intervalo entre cada palabra
    public float rowInterval; // Intervalo entre cada fila

    private string[] rows = new string[]
    {
        "Friday Night Funki",
        "VS",
        "GV"
    };

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned.");
            return;
        }

        StartCoroutine(ShowTextSequence());
    }

    IEnumerator ShowTextSequence()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            ShowRow(rows[i]); // Mostrar la fila
            yield return new WaitForSeconds(rowInterval);

        }
        textMeshPro.gameObject.SetActive(false);
        EventManager.StartFlashEffect(); // Llamar al evento del flash
    }

    void ShowRow(string row)
    {
        textMeshPro.text += row + "\n"; // Añadir la fila al TextMeshPro con un salto de línea
    }
}
