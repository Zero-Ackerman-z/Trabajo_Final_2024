using System;
public class SimplyLinkedList<T>
{
    class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node(T value)
        {
            this.Value = value;
            Next = null;
        }
    }

    private Node Head;
    private int length = 0;

    public void InsertNodeAtStart(T value)
    {
        Node newNode = new Node(value);
        newNode.Next = Head;
        Head = newNode;
        length++;
    }

    public void InsertNodeAtEnd(T value)
    {
        if (Head == null)
        {
            InsertNodeAtStart(value);
        }
        else
        {
            Node tmp = Head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            Node newNode = new Node(value);
            tmp.Next = newNode;
            length++;
        }
    }

    public void InsertNodeAtPosition(T value, int position)
    {
        if (position == 0)
        {
            InsertNodeAtStart(value);
        }
        else if (position == length)
        {
            InsertNodeAtEnd(value);
        }
        else if (position > length)
        {
            throw new System.ArgumentOutOfRangeException("Position is out of range.");
        }
        else
        {
            Node previous = Head;
            for (int i = 0; i < position - 1; i++)
            {
                previous = previous.Next;
            }
            Node newNode = new Node(value);
            newNode.Next = previous.Next;
            previous.Next = newNode;
            length++;
        }
    }

    public void DeleteNodeAtPosition(int position)
    {
        if (position < 0 || position >= length)
        {
            throw new System.ArgumentOutOfRangeException("Position is out of range.");
        }
        else if (position == 0)
        {
            Head = Head.Next;
        }
        else
        {
            Node previous = Head;
            for (int i = 0; i < position - 1; i++)
            {
                previous = previous.Next;
            }
            previous.Next = previous.Next.Next;
        }
        length--;
    }
    public void DeleteNode(T value)
    {
        if (Head == null) return;

        if (AreValuesEqual(Head.Value, value))
        {
            Head = Head.Next;
            length--;
            return;
        }

        Node current = Head;
        while (current.Next != null && !AreValuesEqual(current.Next.Value, value))
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            current.Next = current.Next.Next;
            length--;
        }
    }

    private bool AreValuesEqual(T value1, T value2)
    {
        return value1 != null && value2 != null && value1.ToString() == value2.ToString();

    }




    public int GetLength()
    {
        return length;
    }

    public T GetNodeAtPosition(int position)
    {
        if (position < 0 || position >= length)
        {
            throw new ArgumentOutOfRangeException("Position is out of range.");
        }
        Node current = Head;
        for (int i = 0; i < position; i++)
        {
            current = current.Next;
        }
        return current.Value;
    }
}

