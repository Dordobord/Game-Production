using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner main { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject basicCustomerPrefab;
    [SerializeField] private GameObject criticCustomerPrefab;
    [SerializeField] private GameObject homelessCustomerPrefab;

    [Header("Daily Quota Settings")]
    [SerializeField] private TextMeshProUGUI quotaCountText;
    [SerializeField] private int totalQuota;
    [SerializeField] private int criticLimit;
    [SerializeField] private int homelessLimit;

    [Header("Settings")]
    [SerializeField] private float spawnDelay = 1.5f;

    private List<GameObject> spawnPool = new List<GameObject>();
    [SerializeField] private List<CustomerBehavior> activeCustomers = new List<CustomerBehavior>(); // TODO: serialized for testing

    private float spawnTimer;
    private bool dayActive = false;
    private int quotaCount = 0;

    public int GetQuotaCount() => quotaCount;
    public int GetMaxQuota() => totalQuota;

    void Awake()
    {
        main = this;
    }

    void Update()
    {
        if (!dayActive) return;

        // Display quota
        quotaCountText.text = $"{quotaCount} / {totalQuota}";

        // Check timer if its time to spawn
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            // Check available tables and if there is anyone left to spawn in the spawn pool
            if (TableManager.main.GetFreeTableCount() > 0 && spawnPool.Count > 0)
            {
                SpawnCustomer();
                spawnTimer = 0f;
            }
        }

        // Check if day is over
        if (spawnPool.Count == 0 && activeCustomers.Count == 0)
        {
            EndDay();
        }
    }

    public void StartNewDay()
    {
        GenerateSpawnPool();
        dayActive = true;
        spawnTimer = 0;
        Debug.Log("Day Started. Total customers to serve: " + spawnPool.Count);
    }

    private void GenerateSpawnPool()
    {
        spawnPool.Clear();

        // Add Special Customers
        for (int i = 0; i < criticLimit; i++) spawnPool.Add(criticCustomerPrefab);
        for (int i = 0; i < homelessLimit; i++) spawnPool.Add(homelessCustomerPrefab);

        // Fill the rest with Basic Customers
        int remaining = totalQuota - spawnPool.Count;
        for (int i = 0; i < remaining; i++) spawnPool.Add(basicCustomerPrefab);

        // Shuffle the spawn pool 
        Shuffle(spawnPool);
    }

    private void SpawnCustomer()
    {
        if (spawnPool.Count == 0) return;

        // Get the first prefab from the shuffled list
        GameObject prefabToSpawn = spawnPool[0];
        spawnPool.RemoveAt(0);

        GameObject go = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        CustomerBehavior cb = go.GetComponent<CustomerBehavior>();

        if (cb != null)
        {
            activeCustomers.Add(cb);
        }
    }

    // Call this from CustomerBehavior when they leave/destroy
    public void UnregisterCustomer(CustomerBehavior cb)
    {
        if (activeCustomers.Contains(cb))
        {
            activeCustomers.Remove(cb);
            quotaCount++;
        }
    }

    private void EndDay()
    {
        dayActive = false;
        Debug.Log("Daily Quota Met and all customers left. Day Ending...");

        LevelManager.main.EndDay();
    }

    private void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
