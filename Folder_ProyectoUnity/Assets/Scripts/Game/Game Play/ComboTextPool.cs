using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboTextPool : MonoBehaviour
{
    public TextMeshProUGUI comboTextPrefab;
    public Transform comboTextParent;
    public Vector3 initialPosition; // Posición inicial de los textos de combo
    private CustomQueueManager customQueueManager;

    private void Start()
    {
        customQueueManager = GetComponent<CustomQueueManager>();
    }

    public TextMeshProUGUI GetComboText()
    {
        GameObject dequeuedObject = customQueueManager.DequeueObject();

        if (dequeuedObject != null)
        {
            TextMeshProUGUI text = dequeuedObject.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                text.gameObject.SetActive(true);
                text.transform.SetParent(comboTextParent);
                text.transform.localPosition = initialPosition; // Reinicia la posición del texto
                return text;
            }
            else
            {
                Debug.LogError("El objeto dequeued no tiene componente TextMeshProUGUI.");
                return InstantiateNewText();
            }
        }
        else
        {
            return InstantiateNewText();
        }
    }

    private TextMeshProUGUI InstantiateNewText()
    {
        TextMeshProUGUI newText = Instantiate(comboTextPrefab, comboTextParent);
        newText.transform.localPosition = initialPosition; // Establece la posición inicial
        return newText;
    }

    public void ReturnComboText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
        customQueueManager.EnqueueObject(text.gameObject);
    }
}
