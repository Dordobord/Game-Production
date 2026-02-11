using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner main;
    [SerializeField]private GameObject customerPrefab;
    [SerializeField]private float spawnTimer = 5f;

    bool canSpawn = true;

    void Awake()
    {
        main = this;
    }
    void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), 1f, spawnTimer);
    }

    private void SpawnCustomer()
    {
        if (!canSpawn) return;

        if (TableManager.main.GetFreeTable() == null)
        {
            Debug.Log("No Free tables, not spawning");
            return;
        }

        Debug.Log("Spawning Customer means table is free lmao");
        Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }

    public void StopSpawner()
    {
        canSpawn = false;
        Debug.Log("Spawner is disabled");
    }

    public void StartSpawner()
    {
        canSpawn = true;
        Debug.Log("Spawner is enabled");
    }
}
