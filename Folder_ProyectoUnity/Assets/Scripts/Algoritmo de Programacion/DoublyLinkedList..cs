using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoublyLinkedList<T>
{
    private DoublyLinkedNode<T> head;
    private DoublyLinkedNode<T> tail;
    private int count;

    public int Count { get { return count; } }

    public DoublyLinkedNode<T> Head { get { return head; } }

    public DoublyLinkedNode<T> Tail { get { return tail; } }

    public void AddLast(T data)
    {
        DoublyLinkedNode<T> newNode = new DoublyLinkedNode<T>(data);

        if (count == 0)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Previous = tail;
            tail.Next = newNode;
            tail = newNode;
        }

        count++;
    }

    public DoublyLinkedNode<T> Get(int index)
    {
        if (index < 0 || index >= count)
            return null;

        DoublyLinkedNode<T> current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return current;
    }
}

public class DoublyLinkedNode<T>
{
    public T Data { get; set; }
    public DoublyLinkedNode<T> Next { get; set; }
    public DoublyLinkedNode<T> Previous { get; set; }

    public DoublyLinkedNode(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }
}
