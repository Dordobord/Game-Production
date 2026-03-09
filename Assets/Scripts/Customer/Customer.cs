using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField]private CustomerDataSO data;
    [SerializeField]private GameObject customerIconPrefab;
    [SerializeField]private ItemIconDataBase iconDataBase;
    [SerializeField]private Sprite waitingIcon; 

    public CustomerState State { get; private set; }
    private ItemType orderedItem;
    private Table table;
    private SpriteRenderer sr;
    private Color origCol;
    private Transform[] path;
    private Image orderIcon;
    private int currentpathIndex;
    private bool isSeated;
    private float patienceTimer;

    private Coroutine orderPromptCor;

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

        SpawnOrderIcon();
    }

    private void Update()
    {
        if (State == CustomerState.WalkToTable)
            MoveToSeat();

        if (State == CustomerState.WaitForOrder || State == CustomerState.WaitingForFood)
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

    private void EnterWaitForOrder()
    {
        State = CustomerState.WaitForOrder;
        patienceTimer = data.orderTimer;

        orderPromptCor = StartCoroutine(ShowWaitIcon(2f));
    }

    private IEnumerator ShowWaitIcon(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (State != CustomerState.WaitForOrder || orderIcon == null)
            yield break;

        orderIcon.sprite = waitingIcon;
        orderIcon.enabled = true;
    }

    private void TakeOrder()
    {
        if (orderPromptCor != null)
        {
            StopCoroutine(orderPromptCor);
            orderPromptCor = null;
        }

        State = CustomerState.WaitingForFood;
        patienceTimer = data.serveTimer;
        sr.color = origCol;

        if (orderIcon != null)
        {
            orderIcon.sprite = iconDataBase.GetIcon(orderedItem);
            orderIcon.enabled = true;
        }

        TutorialManager.main?.OnOrderTaken();
    }

    private void ServeFood()
    {
        PlayerInventory inventory = PlayerInventory.main;

        if (inventory == null || !inventory.HasItem(orderedItem))
            return;

        inventory.RemoveItem(orderedItem);

        State = CustomerState.Eating;
        sr.color = origCol;

        if (orderIcon != null) orderIcon.enabled = false;

        StartCoroutine(EatingRoutine());
    }


    private void MoveToSeat()
    {
        if (isSeated) return;

        if (path != null && currentpathIndex < path.Length)
        {
            MoveTo(path[currentpathIndex].position);

            if ((transform.position - path[currentpathIndex].position).sqrMagnitude < 0.01f)
                currentpathIndex++;

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

    private void SpawnOrderIcon()
    {
        if (customerIconPrefab == null) return;

        GameObject iconObj = Instantiate(customerIconPrefab, transform.position + Vector3.up * 2f, Quaternion.identity, transform);

        orderIcon = iconObj.GetComponentInChildren<Image>();
        if (orderIcon != null)
            orderIcon.enabled = false;
    }
}