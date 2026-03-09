using System.Collections;
using NUnit.Framework;
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
    private int currentpathIndex;
    private bool isSeated;

    // Customer
    private CustomerState customerState;
    private ItemMenuData orderedItem;
    private Sprite savedBubbleIcon;
    private int orderCount = 0;
    private bool startedWithMeal = false;
    private bool isExpressing = false;

    public CustomerData CustomerData => customerData;

    public void Interact()
    {
        // Player will take the order if the customer is ready to order
        if (customerState == CustomerState.ReadyToOrder)
        {
            Debug.Log("Customer has ordered: " + orderedItem.dishItemType);

            SetCustomerBubble(orderedItem.dishSprite, true);
            customerState = CustomerState.Waiting;
        }
        else if (customerState == CustomerState.Waiting)
        {
            PlayerInventory inventory = PlayerInventory.main;

            if (inventory == null || !inventory.HasItem(orderedItem.dishItemType))
                return;

            inventory.RemoveItem(orderedItem.dishItemType);

            ChangeState(CustomerState.Eating);

            SetCustomerBubble(null, false);
        }
    }

    void Start()
    {
        // Check if CustomerData is assigned
        if (customerData == null)
        {
            Debug.LogError("CustomerData is missing");
            Destroy(gameObject);
            return;
        }

        // Find a free table for the customer and find the path to the table
        table = TableManager.main.GetFreeTable();
        if (table == null)
        {
            Destroy(gameObject);
            return;
        }

        table.AssignCustomer(this);
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

        // Trigger one-time events when entering a state
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

        Vector3 target = table.SeatPoint.position;
        MoveTo(target);

        // Check if customer has reached the seat
        if ((transform.position - target).sqrMagnitude < 0.01f)
        {
            isSeated = true;
            // Start thinking about the order after being seated
            ChangeState(CustomerState.Thinking);
        }
    }

    private void MoveTo(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, customerData.moveSpeed * Time.deltaTime);
    }

    private IEnumerator ChoosingOrder()
    {
        // Filter the menu based on orderCount
        List<ItemMenuData> availableOptions = new List<ItemMenuData>();

        if (orderCount == 0) // First Order: Side, Drink, or Meal
        {
            availableOptions = MenuList.mainMenu.Where(x =>
                x.category == ItemCategory.Side ||
                x.category == ItemCategory.Drink ||
                x.category == ItemCategory.Main).ToList();
        }
        else if (orderCount == 1) // Second Order: Meal
        {
            availableOptions = MenuList.mainMenu.Where(x => x.category == ItemCategory.Main).ToList();
        }
        else if (orderCount == 2) // Third Order: Dessert or Drink
        {
            availableOptions = MenuList.mainMenu.Where(x =>
                x.category == ItemCategory.Dessert ||
                x.category == ItemCategory.Drink).ToList();
        }

        // If no dessert is unlocked yet, fallback to Drink or just leave
        if (availableOptions.Count == 0)
        {
            Debug.Log("No items available for this course. Customer leaving.");
            ChangeState(CustomerState.Leaving);
            yield break;
        }

        // Select random item from filtered list
        orderedItem = availableOptions[Random.Range(0, availableOptions.Count)];

        // Track if first item was a meal
        if (orderCount == 0 && orderedItem.category == ItemCategory.Main)
        {
            startedWithMeal = true;
        }

        yield return new WaitForSeconds(Random.Range(customerData.thinkingTimer / 2f, customerData.thinkingTimer));

        Debug.Log($"{customerData.customerType} is ready for order #{orderCount + 1}: {orderedItem.dishItemType}");

        SetCustomerBubble(waitingIcon, true);
        customerState = CustomerState.ReadyToOrder;
    }

    private IEnumerator Eating()
    {
        yield return new WaitForSeconds(Random.Range(customerData.eatingTimer / 2f, customerData.eatingTimer));

        // After eating, the customer will leave
        DoneEating();
    }

    private void DoneEating()
    {
        // Handle plates
        if (orderedItem.category != ItemCategory.Drink && orderedItem.category != ItemCategory.Dessert)
            DirtyPlateRack.main.IncreasePlate();

        // Rewards
        LevelManager.main?.AddIncome(orderedItem.price);
        LevelManager.main?.AddExp(customerData.expReward);

        orderCount++;

        // Special logic for Homeless
        if (customerData.customerType == CustomerType.Homeless)
        {
            HomelessBehavior specialBehavior = GetComponent<HomelessBehavior>();
            if (specialBehavior != null) specialBehavior.MoveToSink(this.transform);

            if (table != null)
            {
                table.ClearTable();
                table = null;
            }

            return;
        }

        // Decide if they order again or leave
        bool canOrderAgain = false;

        if (startedWithMeal)
        {
            // If started with meal, they only order twice (Meal -> Dessert/Drink)
            if (orderCount < 2) canOrderAgain = true;
        }
        else
        {
            // If started with Side/Drink, they can order up to 3 times
            if (orderCount < 3) canOrderAgain = true;
        }

        // Add a random chance so they don't always order again
        if (canOrderAgain && Random.value < customerData.orderAgainChance)
        {
            Debug.Log($"{customerData.customerType} will order again");
            ChangeState(CustomerState.Thinking);
            //patienceManager.BoostPatience(); OPTIONAL BOOST
        }
        else
        {
            ChangeState(CustomerState.Leaving);
        }
    }

    public void Fail()
    {
        // Customer loses patience and leaves
        Debug.Log($"{name} has lost patience and is leaving.");

        if (customerData.customerType == CustomerType.FoodCritic)
        {
            // TODO: insert instantly removing 1 star rating
        }

        LevelManager.main?.AddExp(-customerData.expPenalty);
        ChangeState(CustomerState.Leaving);
    }

    private void Leave()
    {
        Debug.Log($"{name} is leaving.");

        if (table != null)
        {
            table.ClearTable();
            table = null;
        }

        CustomerSpawner.main.UnregisterCustomer(this);
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
