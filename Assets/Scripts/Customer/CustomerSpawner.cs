using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner main { get; private set; }
    [SerializeField] private GameObject basicCustomer;
    [SerializeField] private GameObject[] specialCustomer;
    [SerializeField] private float spawnDelay = 0.5f;

    private bool[] canSpawnSpecial; 
    private int spawnQueue = 0;
    private float spawnTimer;
    private bool canSpawn = true;

    void Awake()
    {
        main = this;
    }

    void Update()
    {
        if (!canSpawn) return;

        if (spawnQueue <= 0) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            SpawnCustomer();
            spawnTimer = 0f;
            spawnQueue--;
        }
    }

    public void ResetSpawner()
    {
        canSpawn = true;
        spawnQueue = 0;
        spawnTimer = 0;

        if (specialCustomer != null)
        {
            canSpawnSpecial = new bool[specialCustomer.Length];
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
        if (!canSpawn)
            return;

        spawnQueue++;
        Debug.Log("Queued 1 customer");
    }

    private void SpawnCustomer()
    {
        if (TableManager.main == null) return;
        if (TableManager.main.GetFreeTable() == null) return;

        bool trySpawn = Random.value < 0.2f;

        if (canSpawnSpecial == null || canSpawnSpecial.Length == 0)
        {
            Instantiate(basicCustomer, transform.position, Quaternion.identity);
            return;
        }
        
        if (trySpawn)
        {
            for (int i = 0; i < specialCustomer.Length; i++)
            {
                if (!canSpawnSpecial[i] && specialCustomer[i] != null)
                {
                    Instantiate(specialCustomer[i], transform.position, Quaternion.identity);
                    canSpawnSpecial[i] = true;
                    return;
                }
            }
        }
        Instantiate(basicCustomer, transform.position, Quaternion.identity);
    }

    public void StopSpawning()
    {
        canSpawn = false;
        spawnQueue = 0;
        Debug.Log("Spawner stopped");
    }
}
