using UnityEngine;

public enum CustomerState
{
    WalkToTable,
    WaitForOrder,
    OrderTaken,
    Eating,
    Leaving,
}

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField]private float speed = 2f;
    public CustomerState State {get; private set;}

    Table targetTable;

    void Start()
    {
        targetTable = TableManager.main.GetFreeTable();

        if (targetTable == null)
        {
            Debug.Log("All Table occupied");
            Destroy(gameObject);
            return;
        }

        targetTable.AssignCustomer(this);
        State = CustomerState.WalkToTable;
        Debug.Log("Customer spawned and walking to table");
    }

    void Update()
    {
        if (State == CustomerState.WalkToTable)
        {
            MoveCustomer(targetTable.seatPoint.position);
        }
    }

    private void MoveCustomer(Vector3 target)
    {
        Vector3 pos = transform.position;

        if (Mathf.Abs(target.x - pos.x) > 0.05f)
            pos.x = Mathf.MoveTowards(pos.x, target.x, speed * Time.deltaTime);
        else
            pos.y = Mathf.MoveTowards(pos.y, target.y, speed * Time.deltaTime);
        
        transform.position = pos;

        float distance = Vector3.Distance(transform.position, target);

        if (distance <= 0.1f)
        {
            State = CustomerState.WaitForOrder;
            Debug.Log("Customer is now waiting for their order kek");
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting with Customer");
        Debug.Log("Customer current state: " + State);

        if (State != CustomerState.WaitForOrder)
        {
            Debug.Log("Customer cannot take order right now");
            return;
        }

        State = CustomerState.OrderTaken;
        Debug.Log("Order Taken");
    }

    public void CustomerEating()
    {
        if (State != CustomerState.OrderTaken) return;

        State = CustomerState.Eating;
        Debug.Log("Customer started eating");

        Invoke(nameof(Leave), 3f);
    }

    private void Leave()
    {
        State = CustomerState.Leaving;
        Debug.Log("Customer is done and is leaving");

        Destroy(gameObject, 1f);
    }
}