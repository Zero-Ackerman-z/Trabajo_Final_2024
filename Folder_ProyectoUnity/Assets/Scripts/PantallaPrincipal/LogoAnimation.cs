using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LogoAnimation : BaseAnimation
{
    [Header("Animation Parameters")]
    public float rotationAmount = 10f;
    public float animationSpeed = 1f;
    public float scaleFactor = 1.1f;
    public float animationSpeedScale = 1f;
    public GameObject Fondo;
    protected override void StartAnimation()
    {
        Fondo.SetActive(true);
        base.StartAnimation();

        Vector3 targetPosition = new Vector3(transform.position.x, 1.6f, transform.position.z);
        transform.DOMove(targetPosition, 1.5f).SetEase(Ease.OutQuad);
    }

    void Update()
    {
        if (baseScale == null)
        {
            Debug.LogWarning("baseScale has not been initialized. Call StartAnimation() first.");
            return;
        }
        AnimateRotation(rotationAmount, animationSpeed);
        AnimateScale(scaleFactor, animationSpeedScale);
    }
    


}