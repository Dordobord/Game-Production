using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Transform seatPoint;
    public Transform SeatPoint => seatPoint;

    public bool IsOccupied { get; private set; }
    private Customer currentCustomer;

    public void AssignCustomer(Customer customer)
    {
        currentCustomer = customer;
        IsOccupied = true;
    }
    public void ClearTable()
    {
        currentCustomer = null;
        IsOccupied = false;
    }
}
