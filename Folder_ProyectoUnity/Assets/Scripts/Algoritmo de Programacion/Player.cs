using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GraphControl graphControl;  // Referencia al grafo
    private NodeControl currentNode;   // Nodo actual al que se dirige el jugador
    private MyHashSet<NodeControl> visitedNodes; // Conjunto de nodos visitados
    private MyStack<NodeControl> nodeHistory; // Historial de nodos para permitir retroceso
    private bool isNavigating; // Para evitar que se inicien múltiples navegaciones al mismo tiempo
    private ActionsControllers controls; // Referencia a los controles de entrada

    void Start()
    {
        // Asegurarse de que el GraphControl esté configurado correctamente
        if (graphControl == null)
        {
            Debug.LogError("GraphControl is not assigned. Please assign it in the Inspector.");
            return;
        }

        visitedNodes = new MyHashSet<NodeControl>();
        nodeHistory = new MyStack<NodeControl>();
        isNavigating = false;

        // Configurar los controles de entrada
        controls = new ActionsControllers();
        controls.PaneController.DFJKPanel.performed += ctx => OnDFJKPanelPressed(ctx);
        controls.Enable();

        // Comienza moviéndose al nodo 0
        StartCoroutine(MoveToStartNode());
    }

    void OnDFJKPanelPressed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        // Solo permitir movimientos horizontales o verticales
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0f;
        }
        else
        {
            direction.x = 0f;
        }

        Debug.Log($"DFJK Panel pressed with direction: {direction}");
        if (!isNavigating)
        {
            MoveInDirection(direction);
        }
    }

    void MoveInDirection(Vector2 direction)
    {
        Debug.Log($"Attempting to move in direction: {direction}");
        NodeControl nextNode = GetConnectedNodeInDirection(currentNode, direction);
        if (nextNode != null)
        {
            Debug.Log($"Found node in direction {direction}: {nextNode.nodeIndex}");
            nodeHistory.Push(currentNode); // Añadir el nodo actual al historial
            StartCoroutine(NavigateToNode(nextNode, false)); // false indica que es avance, no retroceso
        }
        else
        {
            Debug.Log($"No node found in direction {direction}");
        }
    }

    NodeControl GetConnectedNodeInDirection(NodeControl node, Vector2 direction)
    {
        DoublyLinkedList<NodeControl> connectedNodes = node.GetConnectedNodes(); // Obtener la lista enlazada de nodos conectados

        DoublyLinkedNode<NodeControl> currentLinkedNode = connectedNodes.Head; // Empezar desde el primer nodo

        while (currentLinkedNode != null)
        {
            NodeControl connectedNode = currentLinkedNode.Data;
            Vector2 toNode = connectedNode.transform.position - node.transform.position;

            // Verificar si el nodo conectado está en la dirección horizontal o vertical especificada
            if (Mathf.Abs(direction.x) > 0 && Mathf.Approximately(toNode.normalized.x, direction.normalized.x))
            {
                Debug.Log($"Connected node found in direction {direction}: {connectedNode.nodeIndex}");
                return connectedNode;
            }
            else if (Mathf.Abs(direction.y) > 0 && Mathf.Approximately(toNode.normalized.y, direction.normalized.y))
            {
                Debug.Log($"Connected node found in direction {direction}: {connectedNode.nodeIndex}");
                return connectedNode;
            }

            currentLinkedNode = currentLinkedNode.Next; // Avanzar al siguiente nodo en la lista enlazada
        }

        Debug.Log($"No connected node found in direction {direction}");
        return null;
    }

    IEnumerator MoveToStartNode()
    {
        // Esperar un segundo para asegurarse de que todos los nodos estén instanciados
        yield return new WaitForSeconds(1);

        // Obtener el primer nodo (nodo 0)
        currentNode = graphControl.GetNodeByIndex(0);
        if (currentNode != null)
        {
            Debug.Log($"Starting at node: {currentNode.nodeIndex}");
            nodeHistory.Push(currentNode); // Añadir el nodo inicial al historial
            StartCoroutine(NavigateToNode(currentNode, false)); // false indica que es avance
        }
        else
        {
            Debug.LogError("Initial node is null. Check node setup.");
        }
    }

    IEnumerator NavigateToNode(NodeControl targetNode, bool isBacktracking)
    {
        Debug.Log($"Navigating to node {targetNode.nodeIndex}");
        isNavigating = true;

        // Mover hacia el nodo objetivo
        while ((targetNode.transform.position - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime * 5f);
            yield return null;
        }

        Debug.Log($"Reached node {targetNode.nodeIndex}");
        currentNode = targetNode;  // Actualiza el nodo actual
        if (!isBacktracking)
        {
            visitedNodes.Add(currentNode); // Añadir el nodo visitado al conjunto
        }

        isNavigating = false;
    }

    private void OnDestroy()
    {
        controls.PaneController.DFJKPanel.performed -= ctx => OnDFJKPanelPressed(ctx);
        controls.Disable();
    }
}
