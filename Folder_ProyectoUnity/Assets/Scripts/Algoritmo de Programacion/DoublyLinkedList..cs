using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoublyLinkedList<T>
{
    private DoublyLinkedNode<T> head;
    private DoublyLinkedNode<T> tail;
    private int count;
    private CustomHashSet<T> dataHashSet; // Usamos nuestra propia estructura

    public int Count { get { return count; } }
    public DoublyLinkedNode<T> Head { get { return head; } }
    public DoublyLinkedNode<T> Tail { get { return tail; } }

    public DoublyLinkedList()
    {
        head = null;
        tail = null;
        count = 0;
        dataHashSet = new CustomHashSet<T>(); // Inicializamos nuestra estructura
    }

    public void AddLast(T data)
    {
        // Verificar si el dato ya está en la lista antes de añadirlo
        if (!Contains(data))
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
            dataHashSet.Add(data); // Añadimos el dato a nuestra estructura personalizada
        }
    }
    public bool Contains(T data)
    {
        return dataHashSet.Contains(data);
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
