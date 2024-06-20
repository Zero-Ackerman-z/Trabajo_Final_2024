using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundTransition : MonoBehaviour
{
    public RectTransform background; // Referencia al RectTransform del fondo
    public float targetYPosition = -500f; // Posición objetivo en Y para la transición
    public float transitionDuration = 1.5f; // Duración de la transición

    private Vector3 originalPosition;

    private void Start()
    {
        // Guardar la posición original del fondo
        originalPosition = background.localPosition;
    }

    public void StartTransition()
    {
        // Mover el fondo a la posición objetivo en el tiempo especificado
        background.DOLocalMoveY(targetYPosition, transitionDuration).SetEase(Ease.OutQuad);
    }

    public void StartTransitionWithCallback(TweenCallback callback)
    {
        // Mover el fondo a la posición objetivo y luego llamar al callback
        background.DOLocalMoveY(targetYPosition, transitionDuration).SetEase(Ease.OutQuad).OnComplete(callback);
    }

    public void ResetBackground()
    {
        // Volver el fondo a su posición original
        background.DOLocalMove(originalPosition, transitionDuration).SetEase(Ease.OutQuad);
    }
}
