using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GraphControl : MonoBehaviour
{
    public GameObject nodePrefab;  
    public TextAsset nodePositionsTxt;  
    public string[] arrayNodePositions;  
    public string[] currentNodePositions;  
    public TextAsset nodeConnectionsTxt;  
    public string[] arrayNodeConnections;  
    public string[] currentNodeConnections;  
    public DoublyLinkedList<NodeControl> allNodes = new DoublyLinkedList<NodeControl>();  

    private void Start()
    {
        Debug.Log("Activando el grafo.");
        CreateNodes();  // Crear nodos
        CreateConnections();  // Crear conexiones

        if (allNodes.Count > 0)
        {
            Debug.Log($"Total nodes created: {allNodes.Count}");
        }
        else
        {
            Debug.LogWarning("No nodes were created.");
        }
    }

    void CreateNodes()
    {
        if (nodePositionsTxt != null)
        {
            arrayNodePositions = nodePositionsTxt.text.Split('\n');  // Leer las posiciones de los nodos
            Debug.Log("Loaded node positions:");

            for (int i = 0; i < arrayNodePositions.Length; i++)
            {
                currentNodePositions = arrayNodePositions[i].Trim().Split(',');  // Dividir cada línea en x e y

                Debug.Log($"Processing line {i + 1}: {arrayNodePositions[i]}");

                if (currentNodePositions.Length < 2)
                {
                    Debug.LogError($"Invalid node position data at line {i + 1}");
                    continue;
                }

                if (float.TryParse(currentNodePositions[0], out float x) && float.TryParse(currentNodePositions[1], out float y))
                {
                    Vector2 position = new Vector2(x, y);  // Crear la posición del nodo
                    GameObject tmp = Instantiate(nodePrefab, position, Quaternion.identity);  // Instanciar el prefab del nodo en la posición dada
                    NodeControl nodeControl = tmp.GetComponent<NodeControl>();

                    if (nodeControl != null)
                    {
                        allNodes.AddLast(nodeControl);
                        nodeControl.nodeIndex = i;  // Asignar el índice del nodo
                        Debug.Log($"Created node at position: {position}");
                    }
                    else
                    {
                        Debug.LogError("Node prefab does not have NodeControl component.");
                    }
                }
                else
                {
                    Debug.LogError($"Failed to parse node position data at line {i + 1}");
                }
            }
        }
        else
        {
            Debug.LogError("nodePositionsTxt is null.");
        }
    }

    void CreateConnections()
    {
        if (nodeConnectionsTxt != null)
        {
            arrayNodeConnections = nodeConnectionsTxt.text.Split('\n');  // Leer las conexiones de los nodos

            for (int i = 0; i < arrayNodeConnections.Length; i++)
            {
                currentNodeConnections = arrayNodeConnections[i].Trim().Split(',');

                if (i >= allNodes.Count)
                {
                    Debug.LogError($"Node index {i} is out of bounds for allNodes list.");
                    continue;
                }

                NodeControl nodeControl = allNodes.Get(i).Data; // Obtener el nodo de la lista enlazada

                Debug.Log($"Processing connections for node {i}: {arrayNodeConnections[i]}");

                for (int j = 0; j < currentNodeConnections.Length; j++)
                {
                    if (int.TryParse(currentNodeConnections[j], out int connectionIndex))
                    {
                        if (connectionIndex < allNodes.Count && connectionIndex != i)
                        {
                            NodeControl targetNode = allNodes.Get(connectionIndex).Data; // Obtener el nodo de destino de la lista enlazada
                            nodeControl.AddConnectedNode(targetNode);
                            targetNode.AddConnectedNode(nodeControl);  // Conexión bidireccional
                            Debug.Log($"Connected node {i} to node {connectionIndex}");
                        }
                        else
                        {
                            Debug.LogError($"Invalid node connection data at line {i + 1}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Failed to parse node connection data at line {i + 1}");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("nodeConnectionsTxt is null.");
        }
    }

    public NodeControl GetNodeByIndex(int index)
    {
        if (index >= 0 && index < allNodes.Count)
        {
            return allNodes.Get(index).Data; // Obtener el nodo de la lista enlazada
        }
        else
        {
            Debug.LogError($"Index {index} is out of bounds for nodes list.");
            return null;
        }
    }
}