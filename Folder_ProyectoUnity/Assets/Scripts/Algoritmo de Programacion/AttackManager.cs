using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject attackPrefab; // Prefab del ataque
    public Transform spawnPoint; // Punto de spawn del ataque

    private MergeSorter sorter; // Instancia del clasificador Merge Sort

    void Start()
    {
        sorter = new MergeSorter();

        Attack[] attacks = new Attack[]
        {
            new Attack("Astrasphere", 5f, Vector3.up),
            new Attack("Voltaic Chains", 3f, Vector3.down),
            new Attack("Lux Calibur", 7f, Vector3.left),
        };

        sorter.Sort(attacks, 0, attacks.Length - 1);

        // Instancia los ataques en orden
        foreach (Attack attack in attacks)
        {
            Instantiate(attackPrefab, spawnPoint.position + attack.direction, Quaternion.identity);
        }
    }
}

