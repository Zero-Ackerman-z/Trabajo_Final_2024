using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyStack<T>
{
    private List<T> items;

    public MyStack()
    {
        items = new List<T>();
    }

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        if (items.Count == 0)
        {
            throw new System.InvalidOperationException("Stack is empty");
        }

        T poppedItem = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return poppedItem;
    }

    public T Peek()
    {
        if (items.Count == 0)
        {
            throw new System.InvalidOperationException("Stack is empty");
        }

        return items[items.Count - 1];
    }

    public int Count
    {
        get { return items.Count; }
    }

    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }
}
