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
        foreach (Table table in tables)
        {
            if (table != null && !table.IsOccupied)
                return table;
        }
        return null;
    }

    public int GetFreeTableCount()
    {
        int count = 0;
        foreach (Table table in tables)
        {
            if (table != null && !table.IsOccupied)
                count++;
        }
        return count;
    }
}
