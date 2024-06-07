using System;
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
}
