using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NodeControl : MonoBehaviour
{
    public int nodeIndex;
    private DoublyLinkedList<NodeControl> connectedNodes = new DoublyLinkedList<NodeControl>();

    public void AddConnectedNode(NodeControl node)
    {
        if (!connectedNodes.Contains(node))
        {
            connectedNodes.AddLast(node);
        }
    }

    public DoublyLinkedList<NodeControl> GetConnectedNodes()
    {
        return connectedNodes;
    }
}
