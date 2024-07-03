using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyHashSet<T>
{
    private Dictionary<T, bool> items;

    public MyHashSet()
    {
        items = new Dictionary<T, bool>();
    }

    public void Add(T item)
    {
        if (!items.ContainsKey(item))
        {
            items.Add(item, true);
        }
    }

    public bool Contains(T item)
    {
        return items.ContainsKey(item);
    }

    public void Remove(T item)
    {
        if (items.ContainsKey(item))
        {
            items.Remove(item);
        }
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
