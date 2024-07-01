using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseAnimation : MonoBehaviour
{
    protected Vector3D baseScale;
    protected Quaternion baseRotation;
    private void OnEnable()
    {
        EventManager.CompleteFlashEvent += StartAnimation;
    }

    private void OnDisable()
    {
        EventManager.CompleteFlashEvent -= StartAnimation;
    }
    protected virtual void StartAnimation()
    {
        baseScale = Vector3D.FromVector3(transform.localScale);
        baseRotation = transform.rotation;
    }
    protected void AnimateRotation(float rotationAmount, float animationSpeed)
    {
        float rotationAngle = Mathf.Sin(Time.time * animationSpeed) * rotationAmount;
        transform.rotation = baseRotation * Quaternion.Euler(0f, 0f, rotationAngle);
    }
    protected void AnimateScale(float scaleFactor, float animationSpeedScale)
    {
        float time = Mathf.PingPong(Time.time * animationSpeedScale, 1f);
        float rhythmicValue = Mathf.Pow(time, 2f);

        Vector3 unityBaseScale = baseScale.ToUnityVector3(); // Convertir baseScale a Vector3 de Unity

        float scaleX = Mathf.Lerp(unityBaseScale.x, unityBaseScale.x * scaleFactor, rhythmicValue);
        float scaleY = Mathf.Lerp(unityBaseScale.y, unityBaseScale.y * scaleFactor, rhythmicValue);
        float scaleZ = unityBaseScale.z; // Mantener Z de baseScale sin cambios

        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
