using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalRaycastArrowDetector : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Transform rayOrigin; // Origen del raycast
    public float rayLength = 10.0f; // Longitud del rayo
    public LayerMask arrowLayerMask; // Máscara de capa para las flechas

    public float minY = 117f; // Posición mínima en el eje Y para considerar la flecha
    public float maxY = 128f; // Posición máxima en el eje Y para considerar la flecha

    [Header("Panel Settings")]
    public GameObject warningPanel; // Panel de advertencia
    public UnityEvent onNoArrowsDetected; // Evento que se dispara cuando no se detectan flechas
    public UnityEvent onArrowsDetected; // Evento que se dispara cuando se detectan flechas

    private float checkInterval = 0.5f; // Intervalo de tiempo entre cada raycast (reducido para una respuesta más rápida)
    private float timeSinceLastArrow = 0f; // Tiempo desde la última detección de una flecha

    private void Start()
    {
        if (onNoArrowsDetected == null)
            onNoArrowsDetected = new UnityEvent();

        if (onArrowsDetected == null)
            onArrowsDetected = new UnityEvent();

        // Suscribirse al evento para abrir y cerrar el panel
        onNoArrowsDetected.AddListener(OpenWarningPanel);
        onArrowsDetected.AddListener(CloseWarningPanel);

        // Desactivar el panel inicialmente
        if (warningPanel != null)
            warningPanel.SetActive(false);

        // Comenzar la detección de flechas
        StartCoroutine(CheckForArrows());
    }

    private IEnumerator CheckForArrows()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            // Realiza el raycast y obtiene todos los hits
            RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin.position, Vector2.right, rayLength, arrowLayerMask);

            // Dibuja el rayo para visualizarlo en la escena
            Debug.DrawRay(rayOrigin.position, Vector2.right * rayLength, Color.red, checkInterval);

            if (hits.Length > 0)
            {
                // Si se detectan flechas, cerrar el panel si está activo
                if (warningPanel != null && warningPanel.activeSelf)
                {
                    CloseWarningPanel();
                }

                timeSinceLastArrow = 0f; // Reiniciar el tiempo ya que se detectaron flechas

                // Usar un bucle `for` en lugar de `foreach`
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    // Loguea información adicional para depurar
                    Debug.Log("Hit collider: " + hit.collider.gameObject.name);
                }
            }
            else
            {
                // Incrementar el tiempo desde la última detección
                timeSinceLastArrow += checkInterval;

                // Si han pasado más de 4 segundos sin detectar flechas, abrir el panel
                if (timeSinceLastArrow >= 4.0f)
                {
                    OpenWarningPanel();
                    timeSinceLastArrow = 0f; // Reiniciar el tiempo para la siguiente comprobación
                }
            }
        }
    }

    private void OpenWarningPanel()
    {
        if (warningPanel != null)
        {
            Debug.Log("No se detectaron flechas. Abriendo panel de advertencia.");
            warningPanel.SetActive(true);
        }
    }

    private void CloseWarningPanel()
    {
        if (warningPanel != null)
        {
            Debug.Log("Flechas detectadas. Cerrando panel de advertencia.");
            warningPanel.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + Vector3.right * rayLength);
    }
}