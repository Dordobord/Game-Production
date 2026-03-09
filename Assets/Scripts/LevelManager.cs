using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LevelData
{
    public int targetIncome;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager main { get; private set; }

<<<<<<< HEAD
    private float currentIncome;
=======
    [SerializeField]private DayTimer dayTimer; 
    [SerializeField]private LevelData[] levels;
    [SerializeField]private int currentLevelIndex = 0;
    [SerializeField]private int totalMoney;
    
    private int currentIncome;
>>>>>>> Kitchen-Stations
    private int currentExp;
    private bool dayEnded;

    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    public float CurrentIncome => currentIncome;
    public int TargetIncome => levels[currentLevelIndex].targetIncome;
    public int CurrentDay => currentLevelIndex + 1;
    public int CurrentExp => currentExp;
    public int TotalMoney => totalMoney;
    public bool IsDayEnded => dayEnded;

    public UnityEvent OnDayEnded;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        playerMovement = Object.FindAnyObjectByType<PlayerMovement>();
        playerStats = Object.FindAnyObjectByType<PlayerStats>();

        StartDay();
    }

    public void StartDay()
    {
        currentIncome = 0;
        currentExp = 0;
        dayEnded = false;

        //dayTimer?.StartTimer();

        CustomerSpawner.main.StartNewDay();

        playerMovement?.AllowMovement(true);
    }

    public void EndDay()
    {
        if (dayEnded) return;

        dayEnded = true;
        OnDayEnded?.Invoke();

        playerMovement?.AllowMovement(false);
    }

    public void NextDay()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Length)
        {
            Debug.Log("No more days!"); currentLevelIndex = levels.Length - 1;
            return;
        }

        Debug.Log("Applying upgrades for next day");
        UpgradeManager.main?.ApplyUpgrades();

        StartDay();
    }

    public void AddIncome(float amount)
    {
        if (dayEnded) return;

        currentIncome += amount;

        totalMoney += amount;
    }

    public void AddExp(int amount)
    {
        if (dayEnded) return;

        currentExp += amount;
        playerStats?.AddExp(amount);
    }

    public bool TrySpendMoney(int amount)
    {
        if (totalMoney < amount) 
            return false;

        totalMoney -= amount;
        return true;
    }
}