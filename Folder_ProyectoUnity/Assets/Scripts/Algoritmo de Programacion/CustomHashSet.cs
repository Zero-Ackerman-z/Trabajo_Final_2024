using System.Collections;
using System.Collections.Generic;
public class CustomHashSet<T>
{
    private List<T> items;

    public CustomHashSet()
    {
        items = new List<T>();
    }

    public void Add(T item)
    {
        if (!Contains(item))
        {
            items.Add(item);
        }
    }

    public bool Contains(T item)
    {
        return items.Contains(item);
    }
}