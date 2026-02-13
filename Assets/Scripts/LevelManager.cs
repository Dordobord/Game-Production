using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int targetIncome;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager main { get; private set; }

    [SerializeField] private LevelData[] levels;
    [SerializeField] private int currentLevelIndex = 0;

    [SerializeField] private int currentIncome = 0;

    private bool levelCompleted;

    public int CurrentIncome => currentIncome;
    public int TargetIncome => levels[currentLevelIndex].targetIncome;
    public int CurrentDay => currentLevelIndex + 1;
    public bool IsLevelCompleted => levelCompleted;

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
        levelCompleted = false;

        Debug.Log($"DAY {CurrentDay} STARTED");

        if (CustomerSpawner.main != null)
        {
            CustomerSpawner.main.ResetSpawner();
            CustomerSpawner.main.SpawnForAvailableTables(); 
        }
    }

    public void AddIncome(int amount)
    {
        if (levelCompleted) return;

        currentIncome += amount;
        Debug.Log($"Income: {currentIncome}");

        if (currentIncome >= TargetIncome)
        {
            CompleteDay();
        }
    }

    private void CompleteDay()
    {
        levelCompleted = true;
        Debug.Log("DAY COMPLETE!");

        CustomerSpawner.main?.StopSpawning();
    }
}
