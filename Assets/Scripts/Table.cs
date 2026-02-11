using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Transform seatPoint;
    public Transform SeatPoint => seatPoint;

    public bool IsOccupied { get; private set; }

    public void AssignCustomer(Customer customer)
    {
        IsOccupied = true;
        Debug.Log($"{name}: Customer seated");
    }

    public void ClearTable()
    {
        IsOccupied = false;
        Debug.Log($"{name}: Table is now FREE");
    }
}
