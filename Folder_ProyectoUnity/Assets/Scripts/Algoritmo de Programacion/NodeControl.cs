using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    public List<NodeControl> ConnectedNodes = new List<NodeControl>();

    void Start()
    {
        Debug.Log($"Node {gameObject.name} position: {transform.position}");
        Debug.Log($"Node {gameObject.name} connected nodes count: {ConnectedNodes.Count}");
        Debug.Log($"Connected nodes of node {gameObject.name}:");
        foreach (var node in ConnectedNodes)
        {
            Debug.Log($"- {node.gameObject.name}");
        }
    }

    public void AddConnectedNode(NodeControl node)
    {
        if (node != null && !ConnectedNodes.Contains(node))
        {
            ConnectedNodes.Add(node);
        }
    }

    // Método para determinar la dirección relativa entre nodos
    public Vector2 GetDirectionTo(NodeControl targetNode)
    {
        Vector2 direction = (targetNode.transform.position - transform.position).normalized;
        return direction;
    }
}

