using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomerSettings
{
    public int criticCount;
    public int homelessCount;
} 

public class DayManager : MonoBehaviour
{
    public static DayManager main { get; private set; }

    [SerializeField] private int currentDay = 0;

    [Header("Day Settings")]
    [Tooltip("Index 0 is Day 0 (Tutorial). Index 1 is Day 1 and so on.")]
    [SerializeField] private List<int> quotaPerDay = new List<int>();
    [SerializeField] private List<CustomerSettings> specialCustomerCount = new List<CustomerSettings>();

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform layoutParent;
    
    public UnityEvent OnDayEnded;
    public bool isPrepPhase { get; private set; }
    private GameObject currentLayout;
    private Transform kitchenStations;
    private Transform officeStations;
    public int FetchCurrentDay() => currentDay;
    private void Awake() => main = this;

    public void InitializeDay(int day, GameObject layoutPrefab)
    {
        PlayerWallet.main?.ResetDayIncome();
        PlayerStats.main?.ResetNewExperienceAndPoints();

        UIGameHUD.main?.UpdateQuota(0, 0);
        UIGameHUD.main?.UpdateDay(day);
        UIGameHUD.main.StartDayButtonVisibility(true);
        currentDay = day;

        bool isLayoutSame = currentLayout == layoutPrefab;

        // Checks if the passed layoutPrefab variable is null and the layout is still the same
        if(layoutPrefab != null && !isLayoutSame)
        {
            // Setting up diner layout based on level and day
            if (currentLayout != null)
                Destroy(currentLayout);

            Debug.Log("Changing layout...");
            currentLayout = Instantiate(layoutPrefab, layoutParent);

            kitchenStations = currentLayout.transform.Find("KitchenStations");
            officeStations = currentLayout.transform.Find("Office");
        }
        else
        {
            Debug.Log("Layout prefab is null or the current layout is the same.");
        }

        PreparationPhase();
    }

    public void SetStations(Transform parent, bool state)
    {
        if (parent == null) return;

        foreach (var interactable in parent.GetComponentsInChildren<IInteractable>())
        {
            //if interactable == Cookbook just skip and continue.
            if (interactable is Cookbook)
                continue;
            (interactable as MonoBehaviour).enabled = state;
        }
    }
    public void PreparationPhase()
    {
        isPrepPhase = true;
        PlayerMovement pm = player.GetComponent<PlayerMovement>();

        pm?.AllowMovement(true);
 
        // TODO: Set office items to be interactable (phone, vault, and clipboard)

        SetStations(kitchenStations, false);
        SetStations(officeStations, true);
    }

    public void StartDay()
    {
        isPrepPhase = false;

        UIGameHUD.main.StartDayButtonVisibility(false);
        
        // TODO:Set office item to be not interactable (phone, vault, and clipboard)
        SetStations(kitchenStations, true);
        SetStations(officeStations, false);
        // Set menu of the day
        MenuHandler.main.SetMainMenu(currentDay);

        // Initialize customer spawner
        bool tutorialDay = currentDay ==0;

        CustomerSpawner.main.InitializeSpawner(
            quotaPerDay[currentDay], 
            specialCustomerCount[currentDay].criticCount,
            specialCustomerCount[currentDay].homelessCount,
            tutorialDay
        );
    }

    public void EndDay()
    {
        OnDayEnded?.Invoke();
    }
}
