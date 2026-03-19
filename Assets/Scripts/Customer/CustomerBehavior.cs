using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public enum CustomerState
{
    Idle,
    Thinking,
    ReadyToOrder,
    Waiting,
    Eating,
    Leaving
}

public class CustomerBehavior : MonoBehaviour, IInteractable
{
    [Header("Customer References")]
    [SerializeField] private CustomerData customerData;
    [SerializeField] private PatienceManager patienceManager;
    [SerializeField] private SpriteRenderer customerSR;

    [Header("Icon Sprites")]
    [SerializeField] private SpriteRenderer iconPlaceHolder;
    [SerializeField] private GameObject iconBackground;
    [SerializeField] private Sprite waitingIcon;

    // Table and pathfinding
    private Table table;
    private Transform[] path;
    private Transform seatPosition;
    private int currentpathIndex;
    private bool isSeated;

    // Customer
    private CustomerState customerState;
    private ItemMenuData orderedItem;
    private Sprite savedBubbleIcon;
    private int orderCount = 0;
    private float bill;
    private bool startedWithMeal = false;
    private bool isExpressing = false;
    private bool isSatisfied = false;

    public CustomerData CustomerData => customerData;

    public void Interact()
    {
        if (customerState == CustomerState.ReadyToOrder)
        {
            Debug.Log("Customer has ordered: " + orderedItem.dishItem);

            SetCustomerBubble(orderedItem.dishSprite, true);
            customerState = CustomerState.Waiting;
        }
        else if (customerState == CustomerState.Waiting)
        {
            PlayerInventory inventory = PlayerInventory.main;

            if (inventory == null || !inventory.HasItem(orderedItem.dishItem))
                return;

            inventory.RemoveItem(orderedItem.dishItem);

            ChangeState(CustomerState.Eating);

            SetCustomerBubble(null, false);
        }
    }

    void Start()
    {
        if (customerData == null)
        {
            Debug.LogError("CustomerData is missing");
            Destroy(gameObject);
            return;
        }

        table = TableManager.main.GetFreeTable();
        if (table == null)
        {
            Destroy(gameObject);
            return;
        }

        seatPosition = table.AssignSeatToCustomer(this);
        customerState = CustomerState.Idle;

        SeatPath seatPath = table.GetComponent<SeatPath>();
        if (seatPath != null && seatPath.Waypoints != null && seatPath.Waypoints.Length > 0)
        {
            path = seatPath.Waypoints;
            currentpathIndex = 0;
        }
    }

    void Update()
    {
        switch (customerState)
        {
            case CustomerState.Idle:
                MoveToSeat();
                break;
            case CustomerState.ReadyToOrder:
                patienceManager.DrainPatience();
                break;
            case CustomerState.Waiting:
                patienceManager.DrainPatience();
                break;
        }
    }

    public void ChangeState(CustomerState newState)
    {
        customerState = newState;

        if (customerState == CustomerState.Thinking)
        {
            StartCoroutine(ChoosingOrder());
        }
        else if (customerState == CustomerState.Eating)
        {
            StartCoroutine(Eating());
        }
        else if (customerState == CustomerState.Leaving)
        {
            Leave();
        }
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

        Vector3 target = seatPosition.position;
        MoveTo(target);

        if ((transform.position - target).sqrMagnitude < 0.01f)
        {
            isSeated = true;
            ChangeState(CustomerState.Thinking);
        }
    }

    private void MoveTo(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, customerData.moveSpeed * Time.deltaTime);
    }

    private IEnumerator ChoosingOrder()
    {
        List<ItemMenuData> availableOptions = new List<ItemMenuData>();

        if (orderCount == 0)
        {
            availableOptions = MenuHandler.main.GetTodayMenu().Where(x =>
                x.category == ItemCategory.Side ||
                x.category == ItemCategory.Drink ||
                x.category == ItemCategory.Main).ToList();
        }
        else if (orderCount == 1)
        {
            availableOptions = MenuHandler.main.GetTodayMenu().Where(x => x.category == ItemCategory.Main).ToList();
        }
        else if (orderCount == 2)
        {
            availableOptions = MenuHandler.main.GetTodayMenu().Where(x =>
                x.category == ItemCategory.Dessert ||
                x.category == ItemCategory.Drink).ToList();
        }

        if (availableOptions.Count == 0)
        {
            Debug.Log("No items available for this course. Customer leaving.");
            ChangeState(CustomerState.Leaving);
            yield break;
        }

        orderedItem = availableOptions[Random.Range(0, availableOptions.Count)];

        if (orderCount == 0 && orderedItem.category == ItemCategory.Main)
        {
            startedWithMeal = true;
        }

        yield return new WaitForSeconds(Random.Range(customerData.thinkingTimer / 2f, customerData.thinkingTimer));

        Debug.Log($"{customerData.customerType} is ready for order #{orderCount + 1}: {orderedItem.dishItem}");

        SetCustomerBubble(waitingIcon, true);
        customerState = CustomerState.ReadyToOrder;
    }

    private IEnumerator Eating()
    {
        yield return new WaitForSeconds(Random.Range(customerData.eatingTimer / 2f, customerData.eatingTimer));
        DoneEating();
    }

    private void DoneEating()
    {
        if (orderedItem.category != ItemCategory.Drink && orderedItem.category != ItemCategory.Dessert)
            DirtyPlateRack.main.IncreasePlate();

        bill += orderedItem.price;
        PlayerStats.main?.AddExp(customerData.expReward);

        orderCount++;

        if (customerData.customerType == CustomerType.Homeless)
        {
            HomelessBehavior specialBehavior = GetComponent<HomelessBehavior>();
            if (specialBehavior != null) specialBehavior.MoveToSink(this.transform);

            if (table != null)
            {
                table.ClearSeat(this);
                table = null;
            }

            return;
        }

        bool canOrderAgain = false;

        if (startedWithMeal)
        {
            if (orderCount < 2) canOrderAgain = true;
        }
        else
        {
            if (orderCount < 3) canOrderAgain = true;
        }

        if (canOrderAgain && Random.value < customerData.orderAgainChance)
        {
            Debug.Log($"{customerData.customerType} will order again");
            ChangeState(CustomerState.Thinking);
        }
        else
        {
            isSatisfied = true;
            ChangeState(CustomerState.Leaving);
        }
    }

    public void Fail()
    {
        Debug.Log($"{name} has lost patience and is leaving.");

        if (customerData.customerType == CustomerType.FoodCritic)
        {
            // TODO: star rating penalty
        }

        PlayerStats.main?.AddExp(-customerData.expPenalty);

        isSatisfied = false;
        ChangeState(CustomerState.Leaving);
    }

    private void Leave()
    {
        Debug.Log($"{name} is leaving.");

        if(isSatisfied)
        {
            // Pay bill
            PlayerWallet.main?.AddIncome(bill);

            // Give tip base on chance
            if(Random.value < customerData.tipChance)
            {
                float baseTip = 0.5f; // TODO: insert tip based on table level
                PlayerWallet.main?.AddIncome(baseTip * customerData.tipMultiplier);
            }
        }

        if (table != null)
        {
            table.ClearSeat(this);
            table = null;
        }

        CustomerSpawner.main.UnregisterCustomer(this, isSatisfied);
        Destroy(gameObject, 1f);
    }

    public IEnumerator ShowTemporaryExpression(Sprite tempIcon, float duration)
    {
        isExpressing = true;
        SetBubbleVisual(tempIcon, true);

        yield return new WaitForSeconds(duration);

        isExpressing = false;
        SetBubbleVisual(savedBubbleIcon, savedBubbleIcon != null);
    }

    public void SetCustomerBubble(Sprite icon, bool showBackground)
    {
        savedBubbleIcon = icon;
        if (!isExpressing)
        {
            SetBubbleVisual(icon, showBackground);
        }
    }

    private void SetBubbleVisual(Sprite icon, bool showBackground)
    {
        if (iconBackground != null)
            iconBackground.SetActive(showBackground);

        if (iconPlaceHolder != null)
            iconPlaceHolder.sprite = icon;
    }
}