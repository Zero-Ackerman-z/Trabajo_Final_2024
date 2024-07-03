using System.Collections;
using UnityEngine;
public class HorizontalRaycastArrowDetector : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Transform rayOrigin; // Origen del raycast
    public float rayLength = 10.0f; // Longitud del rayo
    public LayerMask arrowLayerMask; // M�scara de capa para las flechas

    public float minY = 117f; // Posici�n m�nima en el eje Y para considerar la flecha
    public float maxY = 128f; // Posici�n m�xima en el eje Y para considerar la flecha

    [Header("Panel Settings")]
    public GameObject warningPanel; // Panel de advertencia
    
    private float checkInterval = 0.5f; // Intervalo de tiempo entre cada raycast (reducido para una respuesta m�s r�pida)
    private float timeSinceLastArrow = 0f; // Tiempo desde la �ltima detecci�n de una flecha

    private void Start()
    {

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
                // Si se detectan flechas, cerrar el panel si est� activo
                if (warningPanel != null && warningPanel.activeSelf)
                {
                    CloseWarningPanel();
                }

                timeSinceLastArrow = 0f; // Reiniciar el tiempo ya que se detectaron flechas

                // Usar un bucle `for` en lugar de `foreach`
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    // Loguea informaci�n adicional para depurar
                    Debug.Log("Hit collider: " + hit.collider.gameObject.name);
                }
                EventManager.CountdownComplete();

            }
            else
            {
                // Incrementar el tiempo desde la �ltima detecci�n
                timeSinceLastArrow += checkInterval;

                // Si han pasado m�s de 4 segundos sin detectar flechas, abrir el panel
                if (timeSinceLastArrow >= 2.0f)
                {
                    OpenWarningPanel();
                    timeSinceLastArrow = 0f; // Reiniciar el tiempo para la siguiente comprobaci�n
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