using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Configura��o de Itens")]
    public GameObject[] recyclableItems;    // Arraste os prefabs recicl�veis
    public GameObject[] nonRecyclableItems; // Arraste os n�o-recicl�veis

    [Header("Controle de Spawn")]
    public float spawnRate = 1.0f;      // Tempo entre spawns
    public float initialDelay = 1.0f;   // Tempo antes de come�ar
    public float itemWidthMargin = 0.5f; // Margem de seguran�a para largura

    private float minX, maxX;
    private bool canSpawn = true;

    void Start()
    {
        CalculateScreenBounds();
        InvokeRepeating("SpawnRandomItem", initialDelay, spawnRate);
    }

    void CalculateScreenBounds()
    {
        // M�todo preciso que funciona em qualquer orienta��o
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = bottomLeft.x + itemWidthMargin;
        maxX = topRight.x - itemWidthMargin;
    }

    void SpawnRandomItem()
    {
        if (!canSpawn) return;

        float randomX = Random.Range(minX, maxX);
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

    // M�todo para debug (opcional)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(minX, transform.position.y - 10, 0),
                       new Vector3(minX, transform.position.y + 10, 0));
        Gizmos.DrawLine(new Vector3(maxX, transform.position.y - 10, 0),
                       new Vector3(maxX, transform.position.y + 10, 0));
    }
    // Adicione no ItemSpawner
    public float difficultyIncreaseRate = 10f;
    private float nextDifficultyTime;

    void Update()
    {
        if (Time.time >= nextDifficultyTime)
        {
            spawnRate = Mathf.Max(0.3f, spawnRate * 0.9f); // Aumenta frequ�ncia
            nextDifficultyTime = Time.time + difficultyIncreaseRate;
        }
    }
}