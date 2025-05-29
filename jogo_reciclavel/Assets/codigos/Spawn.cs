using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Configura��o de Itens")]
    public GameObject[] recyclableItems;    // Arraste os prefabs recicl�veis
    public GameObject[] nonRecyclableItems; // Arraste os n�o-recicl�veis

    [Header("Controle de Spawn")]
    public float spawnRate = 1.0f;      // Tempo entre spawns (1 segundo)
    public float xRange = 2.5f;         // Alcance horizontal
    public float initialDelay = 1.0f;   // Tempo antes de come�ar

    private bool canSpawn = true;

    void Start()
    {
        InvokeRepeating("SpawnRandomItem", initialDelay, spawnRate);
    }

    void SpawnRandomItem()
    {
        if (!canSpawn) return;

        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

        // 60% chance de ser recicl�vel
        if (Random.value <= 0.6f && recyclableItems.Length > 0)
        {
            int index = Random.Range(0, recyclableItems.Length);
            Instantiate(recyclableItems[index], spawnPos, Quaternion.identity);
        }
        else if (nonRecyclableItems.Length > 0)
        {
            int index = Random.Range(0, nonRecyclableItems.Length);
            Instantiate(nonRecyclableItems[index], spawnPos, Quaternion.identity);
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
        CancelInvoke("SpawnRandomItem");
    }
}