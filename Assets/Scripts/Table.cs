using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public Transform seatPoint;
    public bool isOccupied;
    private Customer currentCustomer;

    public void AssignCustomer(Customer customer)
    {
        currentCustomer = customer;
        isOccupied = true;
        Debug.Log("Customer is assigned to table");
    }

    public void ClearTable()
    {
        currentCustomer = null;
        isOccupied = false;
        Debug.Log("Table is cleared and free");
    }
    public void Interact()
    {
        Debug.Log("Interacting with table");

        if (currentCustomer == null)
        {
            Debug.Log("Table is empty");
            return;
        }
        
        Debug.Log("Customer state at table: " + currentCustomer.State);

        if (currentCustomer.State == CustomerState.OrderTaken)
        {
            Debug.Log("Serving food to customer");
            currentCustomer.CustomerEating();
        }
        else if (currentCustomer.State == CustomerState.Leaving)
        {
            Debug.Log("Clearting Table");
        }
        else
        {
            Debug.Log("Nothing to do at the table right now");
        }
    }
}