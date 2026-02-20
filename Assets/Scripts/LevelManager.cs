using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int targetIncome;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager main { get; private set; }

    [Header("Level Settings")]
    [SerializeField] private LevelData[] levels;
    [SerializeField] private int currentLevelIndex = 0;

    [Header("References")]
    [SerializeField] private DayTimer dayManager;

    private int currentIncome;
    private int currentExp;
    private bool dayEnded;

    public int CurrentIncome => currentIncome;
    public int TargetIncome => levels[currentLevelIndex].targetIncome;
    public int CurrentDay => currentLevelIndex + 1;
    public int CurrentExp => currentExp;
    public bool IsDayEnded => dayEnded;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        currentIncome = 0;
        currentExp = 0;
        dayEnded = false;

        Debug.Log($"DAY {CurrentDay} STARTED");

        dayManager.StartTimer();

        CustomerSpawner.main?.ResetSpawner();
        CustomerSpawner.main?.SpawnForAvailableTables();

        Object.FindAnyObjectByType<PlayerMovement>()?.AllowMovement(true);
    }

    public void EndDay()
    {
        if (dayEnded) return;

        dayEnded = true;
        Debug.Log("DAY ENDED");

        CustomerSpawner.main?.StopSpawning();
        Object.FindAnyObjectByType<PlayerMovement>()?.AllowMovement(false);
    }

    public void AddIncome(int amount)
    {
        if (dayEnded) return;
        currentIncome += amount;
    }

    public void AddExp(int amount)
    {
        if (dayEnded) return;
        currentExp += amount;
    }
}