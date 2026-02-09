using System;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnTimer = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), 1f, spawnTimer);
    }

    private void SpawnCustomer()
    {
        if (TableManager.main.GetFreeTable() == null)
        {
            Debug.Log("No Free tables, not spawniung");
            return;
        }

        Debug.Log("Spawning Customer");
        Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }
}
