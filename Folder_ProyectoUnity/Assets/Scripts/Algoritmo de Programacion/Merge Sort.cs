using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MergeSorter
{
    // Función para combinar dos subarreglos de arr[]
    // Primer subarreglo es arr[l..m]
    // Segundo subarreglo es arr[m+1..r]
    void Merge(Attack[] arr, int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        // Arreglos temporales
        Attack[] L = new Attack[n1];
        Attack[] R = new Attack[n2];

        // Copiar datos a los arreglos temporales L[] y R[]
        for (int i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (int j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];

        // Combinar los arreglos temporales de vuelta en arr[l..r]
        int k = l;
        int i1 = 0, i2 = 0;
        while (i1 < n1 && i2 < n2)
        {
            if (L[i1].speed <= R[i2].speed)
            {
                arr[k] = L[i1];
                i1++;
            }
            else
            {
                arr[k] = R[i2];
                i2++;
            }
            k++;
        }

        // Copiar los elementos restantes de L[], si hay alguno
        while (i1 < n1)
        {
            arr[k] = L[i1];
            i1++;
            k++;
        }

        // Copiar los elementos restantes de R[], si hay alguno
        while (i2 < n2)
        {
            arr[k] = R[i2];
            i2++;
            k++;
        }
    }

    // Función principal que ordena arr[l..r] usando Merge()
    public void Sort(Attack[] arr, int l, int r)
    {
        if (l < r)
        {
            // Encuentra el punto medio del arreglo
            int m = l + (r - l) / 2;

            // Ordena las primeras y segundas mitades
            Sort(arr, l, m);
            Sort(arr, m + 1, r);

            // Combina las mitades ordenadas
            Merge(arr, l, m, r);
        }
    }
}
public class Attack
{
    public string attackType;
    public float speed;
    public Vector3 direction;

    // Constructor para inicializar los atributos del ataque
    public Attack(string type, float spd, Vector3 dir)
    {
        attackType = type;
        speed = spd;
        direction = dir;
    }
}


