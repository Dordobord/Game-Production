using UnityEngine;
using System.Collections;
using TMPro;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField]private CustomerData data;
    [SerializeField]private GameObject customerTextPrefab;
    public CustomerState State { get; private set; }
    private bool isSeated;
    private float patienceTimer;
    private ItemType orderedItem;
    private Table table;
    private SpriteRenderer sr;
    private Color origCol;
    private TextMeshProUGUI worldText;
    private Coroutine orderPromptCor;
    private Transform[] path;
    private int currentpathIndex;

    private void Start()
    {
        if (data == null)
        {
            Debug.LogError("CustomerData is missing");
            Destroy(gameObject);
            return;    
        }

        sr = GetComponent<SpriteRenderer>();
        sr.color = data.customerCol;
        origCol = sr.color;

        AssignOrder(); 

        table = TableManager.main.GetFreeTable();
        if (table == null)
        {
            Destroy(gameObject);
            return;
        }

        table.AssignCustomer(this);
        State = CustomerState.WalkToTable;

        SeatPath seatPath = table.GetComponent<SeatPath>();
        if (seatPath != null && seatPath.Waypoints != null && seatPath.Waypoints.Length > 0)
        {
            path = seatPath.Waypoints;
            currentpathIndex = 0;
        }

        SpawnCustomerText();
    }

    private void Update()
    {
        if (State == CustomerState.WalkToTable)
            MoveToSeat();

        if (State == CustomerState.WaitForOrder ||
            State == CustomerState.WaitingForFood)
            ManagePatience();
    }

    public void Interact()
    {
        if (State == CustomerState.WaitForOrder)
            TakeOrder();
        else if (State == CustomerState.WaitingForFood)
            ServeFood();
    }

    private void AssignOrder()
    {
        orderedItem = data.menuList[Random.Range(0, data.menuList.Length)];
    }

    private void TakeOrder()
    {
        StopOrderPrompt();

        State = CustomerState.WaitingForFood;
        patienceTimer = data.serveTimer;
        sr.color = origCol;

        if (worldText != null)
            worldText.text = orderedItem.ToString();
        
        TutorialManager.main?.OnOrderTaken();
    }

    private void ServeFood()
    {
        PlayerInventory playerIventory = PlayerInventory.main;

        if (playerIventory == null || !playerIventory.HasItem(orderedItem))
        {
            Debug.Log("Player does not have ordered item");
            return;
        }

        playerIventory.RemoveItem(orderedItem);

        State = CustomerState.Eating;

        sr.color = origCol;

        if (worldText != null)
            worldText.text = "";

        StartCoroutine(EatingRoutine());
    }

    private void MoveToSeat()
    {
        if (isSeated) return;

        if (path != null && currentpathIndex < path.Length)
        {
            MoveTo(path[currentpathIndex].position);

            float distance = (transform.position - path[currentpathIndex].position).sqrMagnitude; 
            if (distance < 0.01f)
            {
                currentpathIndex++;
            }
            
            return;
        }

        Vector3 target = table.SeatPoint.position;
        MoveTo(target);
        if ((transform.position - target).sqrMagnitude < 0.01f)
        {
            isSeated = true;
            EnterWaitForOrder();
        }
    }

    private void MoveTo(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, data.moveSpeed * Time.deltaTime);
    }

    private void EnterWaitForOrder()
    {
        State = CustomerState.WaitForOrder;
        patienceTimer = data.orderTimer;
        orderPromptCor = StartCoroutine(ShowOrderPrompt());
    }

    private IEnumerator ShowOrderPrompt()
    {
        yield return new WaitForSeconds(2f);

        if (State == CustomerState.WaitForOrder && worldText != null)
            worldText.text = "Waiter!";
    }

    private void ManagePatience()
    {
        patienceTimer -= Time.deltaTime;

        if (patienceTimer <= data.warningTimer)
            sr.color = Color.red;

        if (patienceTimer <= 0f)
            FailCustomer();
    }

    private IEnumerator EatingRoutine()
    {
        yield return new WaitForSeconds(5f);

        LevelManager.main?.AddIncome(data.reward);
        LevelManager.main?.AddExp(data.expReward);

        TutorialManager.main?.OnCustomerServed();

        Leave();
    }

    private void FailCustomer()
    {
        LevelManager.main?.AddIncome(-data.penalty);
        LevelManager.main?.AddExp(-data.expPenalty);
        Leave();
    }

    private void Leave()
    {
        State = CustomerState.Leaving;

        if (table != null)
        {
            table.ClearTable();
            table = null;
        }

        CustomerSpawner.main?.SpawnOneCustomer();
        Destroy(gameObject, 1f);
    }

    private void SpawnCustomerText()
    {
        if (customerTextPrefab == null) return;

        GameObject textObj = Instantiate(customerTextPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity, transform);

        worldText = textObj.GetComponentInChildren<TextMeshProUGUI>();
        if (worldText != null)
            worldText.text = "";
    }

    private void StopOrderPrompt()
    {
        if (orderPromptCor != null)
        {
            StopCoroutine(orderPromptCor);
            orderPromptCor = null;
        }
    }
}
