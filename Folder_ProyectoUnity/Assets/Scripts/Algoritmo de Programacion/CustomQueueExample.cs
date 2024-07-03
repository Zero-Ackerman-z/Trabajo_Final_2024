using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomQueueManager : MonoBehaviour
{
    private Queue<GameObject> customQueue = new Queue<GameObject>();

    // Método para agregar objetos a la cola
    public void EnqueueObject(GameObject obj)
    {
        customQueue.Enqueue(obj);
    }

    // Método para obtener y eliminar el primer objeto de la cola
    public GameObject DequeueObject()
    {
        if (customQueue.Count > 0)
        {
            return customQueue.Dequeue();
        }
        else
        {
            Debug.LogWarning("La cola está vacía.");
            return null;
        }
    }

    // Método para verificar si la cola contiene un objeto específico
    public bool ContainsObject(GameObject obj)
    {
        return customQueue.Contains(obj);
    }

    // Método para vaciar la cola
    public void ClearQueue()
    {
        customQueue.Clear();
    }

    // Método para mostrar los elementos de la cola 
    public void PrintQueueItems()
    {
        // Convertir la cola a una matriz para poder acceder a los elementos por índice
        GameObject[] queueArray = customQueue.ToArray();

        // Iterar sobre la matriz usando un bucle for
        for (int i = 0; i < queueArray.Length; i++)
        {
            Debug.Log(queueArray[i].name);
        }
    }
}
