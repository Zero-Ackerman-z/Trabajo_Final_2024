using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LogoAnimation : MonoBehaviour
{
    public float rotationAmount = 10f; // Ángulo de rotación máximo
    public float animationSpeed = 1f; // Velocidad de la animación
    private Quaternion baseRotation; // Rotación original
    public float scaleFactor = 1.1f; // Factor de escala máximo
    public float animationSpeedScale = 1f; // Velocidad de la animación

    private Vector3D baseScale;
    private bool animationStarted = false;

    private void OnEnable()
    {
        EventManager.onFlashComplete += StartAnimation;
    }
    private void Start()
    {
        
    }
    private void OnDisable()
    {
        EventManager.onFlashComplete -= StartAnimation;
    }
    void Update()
    {
        if (animationStarted)
        {
            // Calcular el ángulo de rotación
            float rotationAngle = Mathf.Sin(Time.time * animationSpeed) * rotationAmount;

            transform.rotation = baseRotation * Quaternion.Euler(0f, 0f, rotationAngle);

            float time = Mathf.PingPong(Time.time * animationSpeedScale, 1f);
            float rhythmicValue = Mathf.Pow(time, 2f); // Usar Pow para hacer la curva más pronunciada

            // Calcular el factor de escala rítmico
            float scaleX = Mathf.Lerp(baseScale.X, baseScale.X * scaleFactor, rhythmicValue);
            float scaleY = Mathf.Lerp(baseScale.Y, baseScale.Y * scaleFactor, rhythmicValue);

            // Aplicar la nueva escala al objeto
            transform.localScale = new Vector3(scaleX, scaleY, baseScale.Z);
        }
    }
    public void StartAnimation()
    {
        Debug.Log("StartAnimation llamado. Inicializando baseScale.");

        baseScale = new Vector3D(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        baseRotation = transform.rotation;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, 1.6f, initialPosition.z);
        transform.DOMove(targetPosition, 1.5f).SetEase(Ease.OutQuad);
        animationStarted = true;
    }
}