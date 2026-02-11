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

    private bool levelCompleted = false;

    public int CurrentIncome => currentIncome;
    public int TargetIncome => levels[currentLevelIndex].targetIncome;
    public int CurrentDay => currentLevelIndex + 1;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        currentIncome = 0;
        levelCompleted = false;
    }

    public void AddIncome(int amount)
    {
        if (levelCompleted) return;

        currentIncome += amount;

        Debug.Log("Income updated: " + currentIncome);

        if (currentIncome >= TargetIncome)
        {
            CompleteDay();
        }
    }

    private void CompleteDay()
    {
        levelCompleted = true;

        Debug.Log("DAY COMPLETE!");

        if (CustomerSpawner.main != null)
        {
            CustomerSpawner.main.StopSpawner();
        }
    }
}
