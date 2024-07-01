using System;
using UnityEngine;

public class Vector2D
{
    // Propiedades para las coordenadas X e Y del vector
    public float X { get; set; }
    public float Y { get; set; }

    // Constructor para inicializar las coordenadas X e Y del vector
    public Vector2D(float x, float y)
    {
        X = x;
        Y = y;
    }
    public static Vector2D FromVector2(Vector2 vector)
    {
        return new Vector2D(vector.x, vector.y);
    }

    public Vector2 ToUnityVector2()
    {
        return new Vector2(X, Y);
    }
}