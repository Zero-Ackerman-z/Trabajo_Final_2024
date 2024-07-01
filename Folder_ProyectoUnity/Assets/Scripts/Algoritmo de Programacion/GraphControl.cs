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
    public List<NodeControl> allNodes = new List<NodeControl>();
    private void OnEnable()
    {
        // Suscribirse al evento para activar el grafo cuando se abra el panel de advertencia
    }

    private void OnDisable()
    {
        // Asegurarse de desuscribirse del evento al desactivar el objeto
    }
    private void Start()
    {
        ActivateGraph();
    }
    private void ActivateGraph()
    {
        // Acciones para activar el grafo
        Debug.Log("Activando el grafo.");
        gameObject.SetActive(true); // Ajusta según sea necesario para activar el grafo
        CreateNodes();
        CreateConnections();

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
            arrayNodePositions = nodePositionsTxt.text.Split('\n');
            Debug.Log("Loaded node positions:");
            for (int i = 0; i < arrayNodePositions.Length; i++)
            {
                currentNodePositions = arrayNodePositions[i].Trim().Split(',');

                Debug.Log($"Processing line {i + 1}: {arrayNodePositions[i]}");

                if (currentNodePositions.Length < 2)
                {
                    Debug.LogError("Invalid node position data at line " + (i + 1));
                    continue;
                }

                if (float.TryParse(currentNodePositions[0], out float x) && float.TryParse(currentNodePositions[1], out float y))
                {
                    Vector2 position = new Vector2(x, y);
                    GameObject tmp = Instantiate(nodePrefab, position, Quaternion.identity);
                    NodeControl nodeControl = tmp.GetComponent<NodeControl>();
                    if (nodeControl != null)
                    {
                        allNodes.Add(nodeControl);
                        Debug.Log($"Created node at position: {position}");
                    }
                    else
                    {
                        Debug.LogError("Node prefab does not have NodeControl component.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to parse node position data at line " + (i + 1));
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
            arrayNodeConnections = nodeConnectionsTxt.text.Split('\n');
            for (int i = 0; i < arrayNodeConnections.Length; ++i)
            {
                currentNodeConnections = arrayNodeConnections[i].Trim().Split(',');

                int nodeIndex = i;
                NodeControl nodeControl = allNodes[nodeIndex];

                Debug.Log($"Processing connections for node {nodeIndex}: {arrayNodeConnections[i]}");

                for (int j = 0; j < currentNodeConnections.Length; ++j)
                {
                    if (int.TryParse(currentNodeConnections[j], out int connectionIndex))
                    {
                        if (connectionIndex < allNodes.Count && connectionIndex != nodeIndex)
                        {
                            NodeControl targetNode = allNodes[connectionIndex];
                            nodeControl.AddConnectedNode(targetNode);
                            Debug.Log($"Connected node {nodeIndex} to node {connectionIndex}");
                        }
                        else
                        {
                            Debug.LogError("Invalid node connection data at line " + (i + 1));
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to parse node connection data at line " + (i + 1));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("nodeConnectionsTxt is null.");
        }
    }
}
