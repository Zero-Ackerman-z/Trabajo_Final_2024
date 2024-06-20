using System;
using UnityEngine;
public class Vector3D
{
    // Propiedades para las coordenadas X, Y, Z del vector
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    // Constructor para inicializar las coordenadas X, Y, Z del vector
    public Vector3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Magnitud del vector
    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    // Método para convertir de Vector3 a Vector3D
    public static Vector3D FromVector3(Vector3 vector)
    {
        return new Vector3D(vector.x, vector.y, vector.z);
    }

    // Método para convertir de Vector3D a Vector3
    public Vector3 ToUnityVector3()
    {
        return new Vector3(X, Y, Z);
    }
}
