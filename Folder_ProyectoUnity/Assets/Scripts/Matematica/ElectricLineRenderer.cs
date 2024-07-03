using System.Collections;
using UnityEngine;

public class ElectricLineRenderer : MonoBehaviour
{
    public int pointsCount = 20;
    public float lineHeight = 5f;
    public float displacementRange = 0.2f;
    public float updateSpeed = 0.05f;
    public Gradient colorGradient;
    public float originOffset = 0f; // Offset para ajustar el punto de origen verticalmente

    private LineRenderer lineRenderer;

    void Start()
    {
        // Obtener el LineRenderer del hijo
        lineRenderer = GetComponentInChildren<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer not found in children.");
            return;
        }

        lineRenderer.positionCount = pointsCount;

        // Configurar el gradiente de color
        if (colorGradient != null)
        {
            lineRenderer.colorGradient = colorGradient;
        }

        StartCoroutine(UpdateLine());
    }

    IEnumerator UpdateLine()
    {
        while (true)
        {
            Vector3[] positions = new Vector3[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                float t = (float)i / (pointsCount - 1);
                positions[i] = new Vector3(
                    0f, // Posición X fija en 0 para dibujar verticalmente
                    originOffset - i * (lineHeight / (pointsCount - 1)), // Posición Y ajustable con originOffset
                    0f
                );

                // Aplicar un desplazamiento aleatorio
                positions[i] += new Vector3(
                    Random.Range(-displacementRange, displacementRange),
                    0f,
                    0f
                );
            }

            lineRenderer.SetPositions(positions);
            yield return new WaitForSeconds(updateSpeed);
        }
    }
}
