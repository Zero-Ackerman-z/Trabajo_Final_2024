using System.Collections;
using UnityEngine;
public class MoveToFirstNodeAndNavigate : MonoBehaviour
{
    public GraphControl graphControl; // Referencia al GraphControl
    public float movementSpeed = 5f; // Velocidad de movimiento del jugador

    private bool hasMovedToFirstNode = false; // Bandera para evitar mover repetidamente
    private NodeControl currentNode; // Nodo actual en el que está el jugador
    private NodeControl targetNode; // Próximo nodo objetivo del jugador
    private ActionsControllers controls; // Controlador de acciones del Input System
    private Vector2 inputDirection; // Dirección del input actual

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
        gameObject.SetActive(true); // Ajusta según sea necesario para activar al jugador
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
        // Verifica si GraphControl y la lista de nodos están disponibles
        if (graphControl != null && graphControl.allNodes.Count > 0)
        {
            // Obtén la posición del primer nodo
            currentNode = graphControl.allNodes[0];
            Vector2 targetPosition = currentNode.transform.position;

            // Mueve el jugador a la posición del primer nodo
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            // Verifica si el jugador ha llegado a la posición del primer nodo
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition; // Asegura la posición exacta
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
        // Si hay una dirección de entrada, intenta mover al próximo nodo en esa dirección
        if (inputDirection != Vector2.zero)
        {
            SetNextTargetNode(inputDirection);
        }
    }

    void NavigateToNextNode()
    {
        if (targetNode == null || currentNode == null) return;

        // Mueve el jugador hacia el próximo nodo objetivo
        Vector2 targetPosition = targetNode.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Verifica si el jugador ha llegado a la posición del próximo nodo
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition; // Asegura la posición exacta
            currentNode = targetNode; // Actualiza el nodo actual al nodo objetivo
            targetNode = null; // Reinicia el nodo objetivo
            Debug.Log($"Player moved to node at position: {targetPosition}");
        }
    }

    void SetNextTargetNode(Vector2 direction)
    {
        // Encuentra el nodo conectado que está más cercano en la dirección dada
        NodeControl closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (NodeControl connectedNode in currentNode.ConnectedNodes)
        {
            Vector2 toConnectedNode = (Vector2)connectedNode.transform.position - (Vector2)currentNode.transform.position;
            if (Vector2.Dot(direction, toConnectedNode.normalized) > 0.8f) // Alineación en la dirección
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

    // Método para manejar el input de movimiento
    private void OnMove(Vector2 direction)
    {
        inputDirection = direction;
    }
}
