using System.Collections;
using UnityEngine;
public class MoveToFirstNodeAndNavigate : MonoBehaviour
{
    public GraphControl graphControl; // Referencia al GraphControl
    public float movementSpeed = 5f; // Velocidad de movimiento del jugador

    private bool hasMovedToFirstNode = false; // Bandera para evitar mover repetidamente
    private NodeControl currentNode; // Nodo actual en el que est� el jugador
    private NodeControl targetNode; // Pr�ximo nodo objetivo del jugador
    private ActionsControllers controls; // Controlador de acciones del Input System
    private Vector2 inputDirection; // Direcci�n del input actual

    private void Awake()
    {
        // Configurar el controlador de acciones
        controls = new ActionsControllers();
        controls.PaneController.DFJKPanel.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        controls.PaneController.DFJKPanel.canceled += ctx => OnMove(Vector2.zero); // Detener el movimiento cuando se libera la tecla
    }


     void Start()
    {
        // Acciones para activar al jugador
        Debug.Log("Activando al jugador.");
        gameObject.SetActive(true); // Ajusta seg�n sea necesario para activar al jugador
        MovePlayerToFirstNode();
    }    
        

    


    void Update()
    {
        if (!hasMovedToFirstNode)
        {
            MovePlayerToFirstNode();
        }
        else
        {
            if (targetNode == null)
            {
                HandleInput();
            }
            else
            {
                NavigateToNextNode();
            }
        }
    }

    void MovePlayerToFirstNode()
    {
        // Verifica si GraphControl y la lista de nodos est�n disponibles
        if (graphControl != null && graphControl.allNodes.Count > 0)
        {
            // Obt�n la posici�n del primer nodo
            currentNode = graphControl.allNodes[0];
            Vector2 targetPosition = currentNode.transform.position;

            // Mueve el jugador a la posici�n del primer nodo
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            // Verifica si el jugador ha llegado a la posici�n del primer nodo
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition; // Asegura la posici�n exacta
                hasMovedToFirstNode = true; // Marca como movido
                Debug.Log($"Player moved to the first node at position: {targetPosition}");
            }
        }
        else
        {
            Debug.LogWarning("GraphControl or nodes list is null or empty.");
        }
    }

    void HandleInput()
    {
        // Si hay una direcci�n de entrada, intenta mover al pr�ximo nodo en esa direcci�n
        if (inputDirection != Vector2.zero)
        {
            SetNextTargetNode(inputDirection);
        }
    }

    void NavigateToNextNode()
    {
        if (targetNode == null || currentNode == null) return;

        // Mueve el jugador hacia el pr�ximo nodo objetivo
        Vector2 targetPosition = targetNode.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Verifica si el jugador ha llegado a la posici�n del pr�ximo nodo
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition; // Asegura la posici�n exacta
            currentNode = targetNode; // Actualiza el nodo actual al nodo objetivo
            targetNode = null; // Reinicia el nodo objetivo
            Debug.Log($"Player moved to node at position: {targetPosition}");
        }
    }

    void SetNextTargetNode(Vector2 direction)
    {
        // Encuentra el nodo conectado que est� m�s cercano en la direcci�n dada
        NodeControl closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (NodeControl connectedNode in currentNode.ConnectedNodes)
        {
            Vector2 toConnectedNode = (Vector2)connectedNode.transform.position - (Vector2)currentNode.transform.position;
            if (Vector2.Dot(direction, toConnectedNode.normalized) > 0.8f) // Alineaci�n en la direcci�n
            {
                float distance = toConnectedNode.magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNode = connectedNode;
                }
            }
        }

        if (closestNode != null)
        {
            targetNode = closestNode;
            Debug.Log($"Next target node set to: {targetNode.transform.position}");
        }
        else
        {
            Debug.LogWarning("No connected node found in the desired direction.");
        }
    }

    // M�todo para manejar el input de movimiento
    private void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }
}
