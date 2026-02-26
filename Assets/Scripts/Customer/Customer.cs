using UnityEngine;
using System.Collections;
using TMPro;

public class Customer : MonoBehaviour, IInteractable
{
    [Header("UI")]
    [SerializeField] private GameObject customerTextPrefab;

    [Header("Order Settings")]
    [SerializeField] private ItemType[] menuList; 

    private ItemType orderedItem;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Patience")]
    [SerializeField] private float orderTimer = 15f;
    [SerializeField] private float serveTimer = 15f;
    [SerializeField] private float warningTimer = 5f;

    [Header("Economy")]
    [SerializeField] private int reward = 50;
    [SerializeField] private int penalty = 20;
    [SerializeField] private int expReward = 50;
    [SerializeField] private int expPenalty = 20;

    public CustomerState State { get; private set; }

    private bool isSeated;
    private float patienceTimer;
    private Table table;
    private SpriteRenderer sr;
    private Color origCol;
    private TextMeshProUGUI worldText;
    private Coroutine orderPromptCor;
    private Transform[] path;
    private int currentpathIndex;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        origCol = sr.color;

        RandomizedOrder(); 

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

    private void RandomizedOrder()
    {
        if (menuList == null || menuList.Length == 0)
        {
            Debug.LogError("Customer: Possible Orders list is empty!");
            return;
        }

        orderedItem = menuList[Random.Range(0, menuList.Length)];
    }

    private void TakeOrder()
    {
        StopOrderPrompt();

        State = CustomerState.WaitingForFood;
        patienceTimer = serveTimer;
        sr.color = origCol;

        if (worldText != null)
            worldText.text = orderedItem.ToString();
        
        TutorialManager.main?.OnOrderTaken();
    }

    private void ServeFood()
    {
        PlayerInventory inv = PlayerInventory.main;

        if (inv == null || !inv.HasItem(orderedItem))
        {
            Debug.Log("Player does not have ordered item");
            return;
        }

        inv.RemoveItem(orderedItem);

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

            if ((transform.position - path[currentpathIndex].position).sqrMagnitude < 0.01f) currentpathIndex++;
            
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

    private void MoveTo(Vector3 target)
    {
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.MoveTowards(currentPos.x, target.x, moveSpeed * Time.deltaTime);
        currentPos.y = Mathf.MoveTowards(currentPos.y, target.y, moveSpeed * Time.deltaTime);
        transform.position = currentPos;
    }

    private void EnterWaitForOrder()
    {
        State = CustomerState.WaitForOrder;
        patienceTimer = orderTimer;
        sr.color = origCol;

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

        if (patienceTimer <= warningTimer)
            sr.color = Color.red;

        if (patienceTimer <= 0f)
            FailCustomer();
    }

    private IEnumerator EatingRoutine()
    {
        yield return new WaitForSeconds(5f);

        LevelManager.main?.AddIncome(reward);
        LevelManager.main?.AddExp(expReward);

        TutorialManager.main?.OnCustomerServed();

        Leave();
    }

    private void FailCustomer()
    {
        LevelManager.main?.AddIncome(-penalty);
        LevelManager.main?.AddExp(-expPenalty);
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
