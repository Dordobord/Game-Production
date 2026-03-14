using System.Collections.Generic;
using Unity.VisualScripting;
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

    private GameObject currentLayout;

    public int FetchCurrentDay() => currentDay;

    private void Awake() => main = this;

    public void InitializeDay(int day, GameObject layoutPrefab)
    {
        PlayerWallet.main?.ResetDayIncome();
        PlayerStats.main?.ResetNewExperienceAndPoints();

        UIGameHUD.main?.UpdateQuota(0, 0);
        UIGameHUD.main?.UpdateDay(currentDay);
        UIGameHUD.main.StartDayButtonVisibility(true);
        currentDay = day;

        bool isLayoutSame = currentLayout == layoutPrefab;

        // Checks if the passed layoutPrefab variable is null and the layout is still the same
        if(layoutPrefab != null && !isLayoutSame)
        {
            // Insert setting up diner layout based on level and day
            if (currentLayout != null)
                Destroy(currentLayout);

            Debug.Log("Changing layout...");
            currentLayout = Instantiate(layoutPrefab, layoutParent);
        }
        else
        {
            Debug.Log("Layout prefab is null or the current layout is the same.");
        }

        PreparationPhase();
    }

    public void PreparationPhase()
    {
        PlayerMovement pm = player.GetComponent<PlayerMovement>();

        pm?.AllowMovement(true);

        // TODO: Set office items to be interactable (phone, vault, and clipboard)
    }

    public void StartDay()
    {
        UIGameHUD.main.StartDayButtonVisibility(false);
        
        // TODO:Set office item to be not interactable (phone, vault, and clipboard)

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
