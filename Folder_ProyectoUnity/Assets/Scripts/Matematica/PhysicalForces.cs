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
                // Iterar a trav�s de todos los puntos de contacto en la colisi�n
                for (int i = 0; i < collision.contactCount; i++)
                {
                    // Obtener el punto de contacto actual
                    ContactPoint contact = collision.GetContact(i);

                    // Direcci�n normal de la colisi�n
                    Vector3 normalDirection = contact.normal;

                    // Aplicar fuerza de empuje
                    if (applyPushForce)
                    {
                        Vector3 pushDirection = -normalDirection; // Empujar en direcci�n opuesta a la normal
                        rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                    }
                }
            }
        }

    }
}
