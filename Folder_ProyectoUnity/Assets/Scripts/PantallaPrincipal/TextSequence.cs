using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSequence : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI textMeshPro;

    [Header("Timing Settings")]
    public float wordInterval = 0.5f;
    public float rowInterval = 1.0f;

    [Header("Text Content")]
    public string[] rows = { "Friday Night Funki", "VS", "GV" };

    private void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned.");
            return;
        }

        StartCoroutine(ShowTextSequence());
    }

    private IEnumerator ShowTextSequence()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            ShowRow(rows[i]);
            yield return new WaitForSeconds(rowInterval);
        }
        textMeshPro.gameObject.SetActive(false);
        EventManager.StartFlashEffect();
    }

    private void ShowRow(string row)
    {
        textMeshPro.text += row + "\n";
    }
}