using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalForces : MonoBehaviour
{
    public float pushForce = 10f; // Fuerza de empuje aplicada al colisionar
    public bool applyPushForce = true; // Indica si se debe aplicar una fuerza de empuje

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene el tag "Player"
        if (collision.collider.CompareTag("Player"))
        {
            // Obtener el Rigidbody del objeto que colisiona
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Iterar a través de todos los puntos de contacto en la colisión
                for (int i = 0; i < collision.contactCount; i++)
                {
                    // Obtener el punto de contacto actual
                    ContactPoint contact = collision.GetContact(i);

                    // Dirección normal de la colisión
                    Vector3 normalDirection = contact.normal;

                    // Aplicar fuerza de empuje
                    if (applyPushForce)
                    {
                        Vector3 pushDirection = -normalDirection; // Empujar en dirección opuesta a la normal
                        rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                    }
                }
            }
        }

    }
}
