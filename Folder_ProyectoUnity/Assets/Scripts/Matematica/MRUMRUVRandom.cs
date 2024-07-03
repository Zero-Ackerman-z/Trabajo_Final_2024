using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRUMRUVRandom : MonoBehaviour
{
    public bool isMRUV = false; // Variable booleana para activar MRUV
    public Vector3 initialVelocity = new Vector3(1f, 0f, 0f); // Velocidad inicial en dirección X por defecto
    public float minAcceleration = 1f; // Aceleración mínima en dirección X
    public float maxAcceleration = 4f; // Aceleración máxima en dirección X

    private Vector3 velocity; // Velocidad actual
    private float timeElapsed; // Tiempo transcurrido desde el inicio

    void Start()
    {
        velocity = initialVelocity;
        timeElapsed = 0f;

        // Genera una aceleración aleatoria entre minAcceleration y maxAcceleration si es MRUV
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
        // Movimiento MRUV (Movimiento Rectilíneo Uniformemente Variado)
        else
        {
            float acceleration = Random.Range(minAcceleration, maxAcceleration);
            velocity += new Vector3(acceleration, 0f, 0f) * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

        timeElapsed += Time.deltaTime;
    }

}
