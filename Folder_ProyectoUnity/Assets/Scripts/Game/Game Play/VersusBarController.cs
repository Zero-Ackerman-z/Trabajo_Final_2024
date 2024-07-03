using UnityEngine;
using UnityEngine.UI;

public class VersusBarController : MonoBehaviour
{
    [Header("Bar Settings")]
    public Image bfBar; // Barra para BF
    public Image gvBar; // Barra para GV
    public Image bfIcon; // Icono de BF
    public Image gvIcon; // Icono de GV
    public Sprite bfNormalIcon; // Icono normal de BF
    public Sprite bfLowHealthIcon; // Icono de BF con baja salud
    public Sprite gvNormalIcon; // Icono normal de GV
    public Sprite gvLowHealthIcon; // Icono de GV con baja salud

    private float bfScore;
    private float gvScore;

    [Range(0, 1)]
    public float initialFillAmount = 0.5f; // Cantidad de llenado inicial (0.5 para el centro)
    private const float minimumSpecialScore = 0.5f; // Mínimo permitido en porcentaje para colisiones especiales (5%)
    public GameManager gameManager; // Referencia al GameManager

    private void Start()
    {
        if (bfBar == null || gvBar == null || bfIcon == null || gvIcon == null)
        {
            Debug.LogError("Please assign all required components in VersusBarController!");
            return;
        }

        SetBarSettings(bfBar, Image.OriginHorizontal.Right);
        SetBarSettings(gvBar, Image.OriginHorizontal.Left);

        // Inicializa la barra en el centro
        bfScore = initialFillAmount * 100f; // Comienza con 50%
        gvScore = initialFillAmount * 100f;

        SetFillAmount(initialFillAmount);
    }

    private void SetBarSettings(Image bar, Image.OriginHorizontal fillOrigin)
    {
        if (bar.type != Image.Type.Filled)
        {
            bar.type = Image.Type.Filled;
            bar.fillMethod = Image.FillMethod.Horizontal;
            bar.fillOrigin = (int)fillOrigin;
        }
    }

    public void SetFillAmount(float fillAmount)
    {
        fillAmount = Mathf.Clamp01(fillAmount);

        bfBar.fillAmount = 1 - fillAmount; // BF barra se llena de derecha a izquierda
        gvBar.fillAmount = fillAmount; // GV barra se llena de izquierda a derecha

        UpdateIconsAndMovement(fillAmount);
    }

    public void UpdateScore(float amount, bool isSpecial = false)
    {
        Debug.Log("Actualizando puntuación con cantidad: " + amount);

        // Calcula la nueva puntuación para BF y GV
        bfScore += amount * 100f;
        gvScore -= amount * 100f;

        // Log para depuración
        Debug.Log("Puntuación BF: " + bfScore + " | Puntuación GV: " + gvScore);

        // Lógica para manejar la puntuación mínima basada en el tipo de colisión
        if (isSpecial)
        {
            // Clamping para colisiones especiales, no permitiendo que baje de un mínimo
            bfScore = Mathf.Max(bfScore, minimumSpecialScore);
            gvScore = Mathf.Max(gvScore, minimumSpecialScore);
        }
        else
        {
            // Clamping normal, permitiendo que llegue a 0 solo para BF
            if (bfScore <= 0)
            {
                bfScore = 0;
                // Llama al método de pausa en el GameManager solo cuando BF llega a 0
                gameManager.PauseGame();
            }
            else
            {
                bfScore = Mathf.Clamp(bfScore, 0f, 100f);
            }

            gvScore = Mathf.Clamp(gvScore, 0f, 100f);
        }

        float totalScore = bfScore + gvScore;
        if (totalScore == 0)
        {
            SetFillAmount(0.5f); // Centrar las barras si no hay puntuación
            UpdateIconsAndMovement(0.5f); // Asegurar que los iconos también se centren
            return;
        }

        SetFillAmount(gvScore / totalScore); // Actualiza el llenado de acuerdo a la puntuación
    }
    private void UpdateIcons()
    {
        // Actualiza los sprites de los iconos basándose en fillAmount y puntuación actual
        if (bfScore <= 20f)
        {
            bfIcon.sprite = bfLowHealthIcon;
        }
        else
        {
            bfIcon.sprite = bfNormalIcon;
        }

        if (gvScore <= 20f)
        {
            gvIcon.sprite = gvLowHealthIcon;
        }
        else
        {
            gvIcon.sprite = gvNormalIcon;
        }
    }
    private void UpdateIconsAndMovement(float fillAmount)
    {
        UpdateIcons();

        // Calcula la posición X local de los iconos basándose en el llenado de las barras
        float bfBarWidth = bfBar.rectTransform.rect.width * (1 - bfBar.fillAmount);
        float gvBarWidth = gvBar.rectTransform.rect.width * gvBar.fillAmount;

        // Ajuste para que los iconos estén en el borde del llenado de la barra
        float bfIconX = bfBar.rectTransform.localPosition.x - (bfBar.rectTransform.rect.width / 2) + bfBarWidth;
        float gvIconX = gvBar.rectTransform.localPosition.x - (gvBar.rectTransform.rect.width / 2) + gvBarWidth;

        // Asigna la nueva posición X local a los iconos
        bfIcon.rectTransform.localPosition = new Vector3(bfIconX, bfIcon.rectTransform.localPosition.y, 0f);
        gvIcon.rectTransform.localPosition = new Vector3(gvIconX, gvIcon.rectTransform.localPosition.y, 0f);
        // Cambia el orden en la jerarquía para superponer los iconos
        if (bfScore > gvScore)
        {
            bfIcon.transform.SetSiblingIndex(gvIcon.transform.GetSiblingIndex() + 1);
        }
        else if (gvScore > bfScore)
        {
            gvIcon.transform.SetSiblingIndex(bfIcon.transform.GetSiblingIndex() + 1);

        }
        
    }

}
