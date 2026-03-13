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
    private bool dayStarted = false;
    private bool dayEnded = false;

    public int FetchCurrentDay() => currentDay;

    private void Awake() => main = this;

    public void InitializeDay(int day, GameObject layoutPrefab)
    {
        currentDay = day;

        // Insert setting up diner layout based on level and day
        // if (currentLayout != null)
        //     Destroy(currentLayout);

        // currentLayout = Instantiate(layoutPrefab, layoutParent);
    }

    public void PreparationPhase()
    {
        UIGameHUD.main.UpdateDay(currentDay);
        PlayerMovement pm = player.GetComponent<PlayerMovement>();

        pm?.AllowMovement(true);

        // Set office items to be interactable (phone, vault, and clipboard)
    }

    public void StartDay()
    {
        // Set office item to be not interactable (phone, vault, and clipboard)
        // Set menu of the day
        MenuHandler.main.SetMainMenu(currentDay);

        // Initialize customer spawner
        CustomerSpawner.main.InitializeSpawner(
            quotaPerDay[currentDay], 
            specialCustomerCount[currentDay].criticCount,
            specialCustomerCount[currentDay].homelessCount
        );
    }

    public void EndDay()
    {
        OnDayEnded?.Invoke();
    }
}
