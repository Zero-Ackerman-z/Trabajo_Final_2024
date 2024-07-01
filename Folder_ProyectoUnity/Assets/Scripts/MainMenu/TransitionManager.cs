using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    [Header("Background Settings")]
    public RectTransform background;
    public float targetYPosition = -500f;
    public float transitionDuration = 1.5f;
    private Vector3D originalPosition; // Cambiado a Vector3D

    private void Start()
    {
        originalPosition = Vector3D.FromVector3(background.localPosition); // Convertir de Vector3 a Vector3D
    }

    public void StartBackgroundTransition(TweenCallback onCompleteCallback)
    {
        // Mover el fondo a la posición objetivo y ejecutar el callback al finalizar
        background.DOLocalMoveY(targetYPosition, transitionDuration)
                  .SetEase(Ease.OutQuad)
                  .OnComplete(onCompleteCallback);
    }

    public void ResetBackground()
    {
        // Volver el fondo a su posición original
        background.DOLocalMove(originalPosition.ToUnityVector3(), transitionDuration).SetEase(Ease.OutQuad);
    }
}