using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LogoAnimation : MonoBehaviour
{
    public float rotationAmount = 10f;
    public float animationSpeed = 1f;
    private Quaternion baseRotation;
    public float scaleFactor = 1.1f;
    public float animationSpeedScale = 1f;

    private Vector3D baseScale;

    private void OnEnable()
    {
        EventManager.onFlashComplete += StartAnimation;
    }

    private void OnDisable()
    {
        EventManager.onFlashComplete -= StartAnimation;
    }

    void Update()
    {
        if (baseScale == null)
        {
            Debug.LogWarning("baseScale has not been initialized. Call StartAnimation() first.");
            return;
        }

        float rotationAngle = Mathf.Sin(Time.time * animationSpeed) * rotationAmount;
        transform.rotation = baseRotation * Quaternion.Euler(0f, 0f, rotationAngle);

        float time = Mathf.PingPong(Time.time * animationSpeedScale, 1f);
        float rhythmicValue = Mathf.Pow(time, 2f);

        Vector3 unityBaseScale = baseScale.ToUnityVector3(); // Convertir baseScale a Vector3 de Unity

        float scaleX = Mathf.Lerp(unityBaseScale.x, unityBaseScale.x * scaleFactor, rhythmicValue);
        float scaleY = Mathf.Lerp(unityBaseScale.y, unityBaseScale.y * scaleFactor, rhythmicValue);
        float scaleZ = unityBaseScale.z; // Mantener Z de baseScale sin cambios

        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }


    public void StartAnimation()
    {
        baseScale = Vector3D.FromVector3(transform.localScale);
        baseRotation = transform.rotation;

        Vector3 targetPosition = new Vector3(transform.position.x, 1.6f, transform.position.z);
        transform.DOMove(targetPosition, 1.5f).SetEase(Ease.OutQuad);
    }
}