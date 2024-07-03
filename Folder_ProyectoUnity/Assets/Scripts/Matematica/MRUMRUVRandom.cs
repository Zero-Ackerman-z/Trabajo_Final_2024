using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRUMRUVRandom : MonoBehaviour
{
    public bool isMRUV = false; // Variable booleana para activar MRUV
    public Vector3 initialVelocity = new Vector3(1f, 0f, 0f); // Velocidad inicial en direcci�n X por defecto
    public float minAcceleration = 1f; // Aceleraci�n m�nima en direcci�n X
    public float maxAcceleration = 4f; // Aceleraci�n m�xima en direcci�n X

    private Vector3 velocity; // Velocidad actual
    private float timeElapsed; // Tiempo transcurrido desde el inicio

    void Start()
    {
        velocity = initialVelocity;
        timeElapsed = 0f;

        // Genera una aceleraci�n aleatoria entre minAcceleration y maxAcceleration si es MRUV
        if (isMRUV)
        {
            float randomAcceleration = Random.Range(minAcceleration, maxAcceleration);
            velocity.x += randomAcceleration;
        }
    }

    void Update()
    {
        // Movimiento MRU 
        if (!isMRUV)
        {
            transform.position += velocity * Time.deltaTime;
        }
        // Movimiento MRUV (Movimiento Rectil�neo Uniformemente Variado)
        else
        {
            float acceleration = Random.Range(minAcceleration, maxAcceleration);
            velocity += new Vector3(acceleration, 0f, 0f) * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

        timeElapsed += Time.deltaTime;
    }

}
