using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager main { get; private set; }

    [SerializeField] private List<Table> tables = new List<Table>();

    void Awake()
    {
        main = this;
    }

    public Table GetFreeTable()
    {
        List<Table> freeTables = new List<Table>();
        foreach (Table table in tables)
        {
            if (table != null && !table.IsTableFull)
                freeTables.Add(table);
        }

        int randomFreeTable = Random.Range(0, freeTables.Count);

        return freeTables[randomFreeTable];
    }

    public int GetFreeTableCount()
    {
        int count = 0;
        foreach (Table table in tables)
        {
            if (table != null && !table.IsTableFull)
                count++;
        }
        return count;
    }
}
