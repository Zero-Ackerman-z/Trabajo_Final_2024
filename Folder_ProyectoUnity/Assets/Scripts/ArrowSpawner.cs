using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowSpawner : MonoBehaviour
{
    public GameObject levelPrefab; // El prefab del nivel que contiene las flechas
    public Transform spawnPoint; // Punto de spawn del prefab
    public bool isUpScroll = true; // Determina si el scroll es hacia arriba o hacia abajo
    public AudioClip musicClip;
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

        Arrow[] arrows = levelInstance.GetComponentsInChildren<Arrow>(); // Obt�n las flechas instanciadas
        
    }


}

