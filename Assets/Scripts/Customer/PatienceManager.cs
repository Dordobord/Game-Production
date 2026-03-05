using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
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
        currentPatience = Random.Range(customerData.minPatience, customerData.maxPatience);
        currentMaxPatience = currentPatience;
        drainRate = customerData.patienceDecreaseRate;
    }

    void Update()
    {
        float patiencePercent = currentPatience / currentMaxPatience;

        if (patiencePercent <= angryThreshold && currentState != PatienceState.Angry)
        {
            TriggerExpression(PatienceState.Angry, angryExpression);
        }
        else if (patiencePercent <= frustratedThreshold && patiencePercent > angryThreshold && currentState != PatienceState.Frustrated)
        {
            TriggerExpression(PatienceState.Frustrated, frustratedExpression);
        }
        else if (patiencePercent <= happyThreshold && patiencePercent > frustratedThreshold && currentState != PatienceState.Happy)
        {
            TriggerExpression(PatienceState.Happy, happyExpression);
        }
    }

    private void TriggerExpression(PatienceState newState, Sprite icon)
    {
        currentState = newState;
        StartCoroutine(customerBehavior.ShowTemporaryExpression(icon, showDuration));

    }

    public void DrainPatience()
    {
        if (currentPatience > 0)
            currentPatience -= drainRate * Time.deltaTime;
        else
            customerBehavior.Fail();

    }

    public void BoostPatience()
    {
        float boostAmount = boostPercent * currentMaxPatience;
        currentPatience += boostAmount;

        float patiencePercent = currentPatience / currentMaxPatience;

        if (patiencePercent >= angryThreshold && currentState != PatienceState.Frustrated)
        {
            currentState = PatienceState.Frustrated;
        }
        else if (patiencePercent >= frustratedThreshold && patiencePercent < angryThreshold && currentState != PatienceState.Happy)
        {
            currentState = PatienceState.Angry;
        }
        else if (patiencePercent >= happyThreshold && patiencePercent < frustratedThreshold && currentState != PatienceState.Initial)
        {
            currentState = PatienceState.Initial;
        }
    }
}
