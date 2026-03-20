using UnityEngine;

enum PatienceState
{
    Initial,
    Happy,
    Frustrated,
    Angry
}

public class PatienceManager : MonoBehaviour
{
    [SerializeField] private UpgradeSO chairUpgrade; //reference

    [Header("Customer Behavior Script Reference")]
    [SerializeField] private CustomerBehavior customerBehavior;

    [Header("Patience Settings")]
    [SerializeField] private float boostPercent = 0.2f;
    [SerializeField] private float showDuration = 2f;
    [SerializeField] private float happyThreshold = 0.90f;
    [SerializeField] private float frustratedThreshold = 0.60f;
    [SerializeField] private float angryThreshold = 0.30f;

    [Header("Icon References")]
    [SerializeField] private Sprite happyExpression;
    [SerializeField] private Sprite frustratedExpression;
    [SerializeField] private Sprite angryExpression;

    private CustomerData customerData;
    [SerializeField] private float currentPatience; // serialized for testing
    private float currentMaxPatience;
    private float drainRate;

    private PatienceState currentState = PatienceState.Initial;

    void Start()
    {
        if (customerBehavior == null)
        {
            Debug.LogError("CustomerBehavior component is missing");
            Destroy(gameObject);
            return;
        }

        customerData = customerBehavior.CustomerData;

        // Initialize customer
        if (customerData == null) return;

        float basePatience = Random.Range(customerData.minPatience, customerData.maxPatience);

        float bonus = 0f;

        if (chairUpgrade != null && chairUpgrade.IsUnlocked)
        {
            bonus = chairUpgrade.GetValue();
        }

        currentMaxPatience = basePatience + bonus;
        currentPatience = currentMaxPatience;

        drainRate = customerData.patienceDecreaseRate;
    }

    void Update()
    {
        float patiencePercent;

        if (currentMaxPatience > 0)
        {
            patiencePercent = currentPatience / currentMaxPatience;
        }
        else
        {
            patiencePercent = 0f;
        }

        UpdateState(patiencePercent);
    }

    public void DrainPatience()
    {
        currentPatience -= drainRate * Time.deltaTime;
        currentPatience = Mathf.Max(0f, currentPatience); // doesnt go beyond negatives

        if (currentPatience <= 0f)
        {
            customerBehavior.Fail();
        }

    }

    public void BoostPatience()
    {
        float boostAmount = boostPercent * currentMaxPatience;
        currentPatience = Mathf.Min(currentMaxPatience, currentPatience + boostAmount); //wont go above the max limit
        float patiencePercent;

        if (currentMaxPatience > 0)
        {
            patiencePercent = currentPatience / currentMaxPatience;
        }
        else
        {
            patiencePercent = 0f;
        }

        UpdateState(patiencePercent);
    }

    private void UpdateState(float patiencePercent)
    {
        PatienceState newState;

        if (patiencePercent >= happyThreshold)
            newState = PatienceState.Initial;
        else if (patiencePercent >= frustratedThreshold)
            newState = PatienceState.Happy;
        else if (patiencePercent >= angryThreshold)
            newState = PatienceState.Frustrated;
        else
            newState = PatienceState.Angry;

        if (newState == currentState) return;

        currentState = newState;

        Sprite icon = null;

        if (newState == PatienceState.Happy)
            icon = happyExpression;
        else if (newState == PatienceState.Frustrated)
            icon = frustratedExpression;
        else if (newState == PatienceState.Angry)
            icon = angryExpression;

        if (icon != null)
            StartCoroutine(customerBehavior.ShowTemporaryExpression(icon, showDuration));
    }
}
