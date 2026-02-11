using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float eatDuration = 5f;
    [SerializeField] private int incomeReward = 50;

    public CustomerState State { get; private set; }

    private Table targetTable;
    private bool isSeated;

    void Start()
    {
        targetTable = TableManager.main.GetFreeTable();

        if (targetTable == null)
        {
            Debug.Log("No available tables.");
            Destroy(gameObject);
            return;
        }

        targetTable.AssignCustomer(this);
        ChangeState(CustomerState.WalkToTable);
    }

    void Update()
    {
        if (State == CustomerState.WalkToTable && targetTable != null)
        {
            MoveToSeat(targetTable.SeatPoint.position);
        }
    }

    private void MoveToSeat(Vector3 targetPos)
    {
        if (isSeated) return;

        Vector3 currentPos = transform.position;

        if (Mathf.Abs(targetPos.x - currentPos.x) > 0.05f)
        {
            currentPos.x = Mathf.MoveTowards(currentPos.x,targetPos.x,moveSpeed * Time.deltaTime);
        }
        else
        {
            currentPos.y = Mathf.MoveTowards(currentPos.y, targetPos.y, moveSpeed * Time.deltaTime);
        }

        transform.position = currentPos;

        if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            isSeated = true;
            Debug.Log("Customer seated.");
            ChangeState(CustomerState.WaitForOrder);
        }
    }
    public void Interact()
    {
        Debug.Log($"State: {State}");

        switch (State)
        {
            case CustomerState.WaitForOrder:
                Debug.Log("Order taken.");
                ChangeState(CustomerState.WaitingForFood);
                break;

            case CustomerState.WaitingForFood:
                Debug.Log("Serving dish.");
                ServeDish();
                break;

            default:
                Debug.Log("Nothing to do");
                break;
        }
    }
    public void ServeDish()
    {
        if (State != CustomerState.WaitingForFood) return;

        ChangeState(CustomerState.Eating);

        StartCoroutine(EatRoutine());

        IEnumerator EatRoutine()
        {
            yield return new WaitForSeconds(eatDuration);

            if (LevelManager.main != null)
            {
                LevelManager.main.AddIncome(incomeReward);
            }

            Leave();
        }
    }
    private void Leave()
    {
        ChangeState(CustomerState.Leaving);

        if (targetTable != null)
        {
            targetTable.ClearTable();
            targetTable = null;
        }

        Destroy(gameObject, 1f);
    }

    private void ChangeState(CustomerState newState)
    {
        State = newState;
        Debug.Log(State);
    }
}
