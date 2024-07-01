using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public float selectedScale = 1.2f;
    public float defaultScale = 1f;
    public float moveDistance = 30f; 

    private RectTransform buttonRectTransform;
    private Vector3D originalPosition; 

    private void Awake()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        originalPosition = new Vector3D(buttonRectTransform.localPosition.x,
                                        buttonRectTransform.localPosition.y,
                                        buttonRectTransform.localPosition.z);
    }

    public void AnimateButton(bool isSelected)
    {
        // Determinar la escala y la posición del botón basados en estado  selección
        float scale = isSelected ? selectedScale : defaultScale;
        float targetX = isSelected ? originalPosition.X + moveDistance : originalPosition.X;

        // Aplicar la animación de escala y movimiento
        buttonRectTransform.DOScale(scale, 0.2f).SetEase(Ease.OutQuad);
        buttonRectTransform.DOLocalMoveX(targetX, 0.2f).SetEase(Ease.OutQuad);
    }
    public void HideButton()
    {
        // Desaparecer el botón
        buttonRectTransform.DOScale(0, 0.2f).SetEase(Ease.InQuad).OnComplete(() => gameObject.SetActive(false));
    }

    public void ResetPosition()
    {
        // Restaurar la posición original del botón
        buttonRectTransform.DOLocalMove(originalPosition.ToUnityVector3(), 0.2f).SetEase(Ease.OutQuad);
    }
}