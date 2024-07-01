using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowSpawner : MonoBehaviour
{
    public GameObject levelPrefab; // El prefab del nivel que contiene las flechas
    public Transform spawnPoint; // Punto de spawn del prefab
    public bool isUpScroll = true; // Determina si el scroll es hacia arriba o hacia abajo
    private void Start()
    {
        SpawnArrows();
    }
    public void SpawnArrows()
    {
        if (levelPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Prefab del nivel o punto de spawn no asignado.");
            return;
        }

        // Instanciar el prefab del nivel en el punto de spawn
        GameObject levelInstance = Instantiate(levelPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);

        // Configurar las flechas para que se muevan hacia el objetivo (implementa tu lógica aquí)
        Arrow[] arrows = levelInstance.GetComponentsInChildren<Arrow>(); // Obtén las flechas instanciadas
        
    }


}

