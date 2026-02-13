using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner main { get; private set; }
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private float spawnDelay = 0.5f;

    private int spawnQueue = 0;
    private float spawnTimer;
    private bool spawningEnabled = true;

    void Awake()
    {
        main = this;
    }

    void Update()
    {
        if (!spawningEnabled) return;

        if (spawnQueue <= 0) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            SpawnCustomer();
            spawnTimer = 0f;
            spawnQueue--;
        }
    }

    public void SpawnForAvailableTables()
    {
        if (TableManager.main == null) return;

        int freeTables = TableManager.main.GetFreeTableCount();

        spawnQueue += freeTables;

        Debug.Log($"Queued {freeTables} customers for spawn");
    }

    public void SpawnOneCustomer()
    {
        if (!spawningEnabled)
            return;

        spawnQueue++;
        Debug.Log("Queued 1 customer");
    }

    private void SpawnCustomer()
    {
        if (TableManager.main == null) return;

        if (TableManager.main.GetFreeTable() == null) return;

        Instantiate(customerPrefab, transform.position, Quaternion.identity);
        Debug.Log("Customer spawned");
    }

    public void StopSpawning()
    {
        spawningEnabled = false;
        spawnQueue = 0;
        Debug.Log("Spawner stopped");
    }

    public void ResetSpawner()
    {
        spawningEnabled = true;
        spawnQueue = 0;
        spawnTimer = 0f;
        Debug.Log("Spawner reset");
    }
}
