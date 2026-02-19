using UnityEngine;
using System.Collections;
using System;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField]private float moveSpeed = 2f;
    [SerializeField]private float eatDuration = 5f;
    [SerializeField]private int incomeReward = 50;
    [SerializeField]private float orderTimer = 15f;
    [SerializeField]private float serveTimer = 15f;
    [SerializeField]private float warningTimer = 5f;
    [SerializeField]private int penalty = 20;

    public CustomerState State { get; private set; }

    private Table targetTable;
    private SpriteRenderer sr;
    private Color origCol;
    private float patienceTimer;
    private bool isSeated;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        origCol = sr.color;

        targetTable = TableManager.main.GetFreeTable();

        if (targetTable == null)
        {
            Debug.Log("No free tables available.");
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

        if (State == CustomerState.WaitForOrder || State == CustomerState.WaitingForFood)
        {
            Patience();
        }
    }

    private void MoveToSeat(Vector3 targetPos)
    {
        if (isSeated) return;

        Vector3 currentPos = transform.position;

        if (Mathf.Abs(targetPos.x - currentPos.x) > 0.05f)//move x if currentPos it still away from target
        {
            currentPos.x = Mathf.MoveTowards(currentPos.x, targetPos.x,moveSpeed * Time.deltaTime);
        }
        else//go down if near target
        {
            currentPos.y = Mathf.MoveTowards(currentPos.y, targetPos.y, moveSpeed * Time.deltaTime);
        }
        transform.position = currentPos;

        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance <= 0.1f)
        {
            isSeated = true;
            Debug.Log("Customer is seated");
            ChangeState(CustomerState.WaitForOrder);
        }
    }

    public void Interact()
    {
        Debug.Log($"Customer State: {State}");

        switch (State)
        {
            case CustomerState.WaitForOrder:
                Debug.Log("Order taken");
                ChangeState(CustomerState.WaitingForFood);
                break;

            case CustomerState.WaitingForFood:
                Debug.Log("Serving dish (test)");
                ServeDish();
                break;

            default:
                Debug.Log("Nothing to do");
                break;
        }
    }

    private void Patience()
    {
        patienceTimer -= Time.deltaTime;

        if (patienceTimer <= warningTimer)
        {
            sr.color = Color.red;
        }

        if (patienceTimer <= 0)
        {
            FailToServe();
        }
    }

    private void FailToServe()
    {
        Debug.Log("Failed to server customer womp womp");
        LevelManager.main?.AddIncome(-penalty);
        Leave();
    }

    private void ServeDish()
    {
        if (State != CustomerState.WaitingForFood) return;

        ChangeState(CustomerState.Eating);
        StartCoroutine(EatRoutine());
    }

    private IEnumerator EatRoutine()
    {
        yield return new WaitForSeconds(eatDuration);

        if (LevelManager.main != null)
        {
            LevelManager.main.AddIncome(incomeReward);
        }

        Leave();
    }

    private void Leave()
    {
        ChangeState(CustomerState.Leaving);

        if (targetTable != null)
        {
            targetTable.ClearTable();
            targetTable = null;
        }

        if (CustomerSpawner.main != null)
        {
            CustomerSpawner.main.SpawnOneCustomer();
        }

        Destroy(gameObject, 1f);
    }

    private void ChangeState(CustomerState newState)
    {
        State = newState;

        sr.color = origCol; //resets color
        
        switch (newState)
        {
            case CustomerState.WaitForOrder:
                patienceTimer = orderTimer;
                break;
            case CustomerState.WaitingForFood:
                patienceTimer = serveTimer;
                break;
        }
    }
}
