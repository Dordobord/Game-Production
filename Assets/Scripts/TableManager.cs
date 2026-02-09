using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager main;
    public List<Table> tables;

    void Awake()
    {
        if (main != null)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    public Table GetFreeTable()
    {
        foreach (Table table in tables)
        {
            if (!table.isOccupied)
                return table;
        }
        return null;
    }
}
