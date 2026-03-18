using System.Collections.Generic;
using UnityEngine;

public class GameProgression : MonoBehaviour
{
    public static GameProgression main;

    [System.Serializable]
    public class ProgressionUnlock
    {
        public int level;
        public int day;

        [Header("Unlock Stations")]
        public List<GameObject> stationsToUnlock;

        [Header("ItemMenuData")]
        public List<ItemMenuData> menuItemsToUnlock;

        [Header("Unlock Upgrades")]
        public List<RestaurantUpgradeSO> upgradesToUnlock;
    }

    [Header("Progression Data")]
    [SerializeField] private List<ProgressionUnlock> unlocks = new List<ProgressionUnlock>();

    private HashSet<string> unlockedProgressions = new HashSet<string>();

    private void Awake()
    {
        main = this;
    }

    public void CheckProgression(int currentLevel, int currentDay)
    {
        foreach (var unlock in unlocks)
        {
            if (unlock.level == currentLevel && unlock.day == currentDay)
            {
                string progressionKey = $"{unlock.level}-{unlock.day}";

                if (unlockedProgressions.Contains(progressionKey))
                    return;

                unlockedProgressions.Add(progressionKey);

                UnlockStations(unlock);
                UnlockMenuItems(unlock);
                UnlockUpgrades(unlock);

                Debug.Log($"Progression triggered: Level {currentLevel} Day {currentDay}");
            }
        }
    }

    private void UnlockStations(ProgressionUnlock unlock)
    {
        foreach (GameObject station in unlock.stationsToUnlock)
        {
            if (station == null) continue;

            station.SetActive(true);
            Debug.Log("Station Unlocked: " + station.name);
        }
    }

    private void UnlockMenuItems(ProgressionUnlock unlock)
    {
        foreach (ItemMenuData item in unlock.menuItemsToUnlock)
        {
            if (item == null) continue;

            if (!MenuHandler.main.mainMenu.Contains(item))
            {
                MenuHandler.main.mainMenu.Add(item);
                Debug.Log("Menu Item Unlocked: " + item.name);
            }
        }
    }

    private void UnlockUpgrades(ProgressionUnlock unlock)
    {
        foreach (RestaurantUpgradeSO upgrade in unlock.upgradesToUnlock)
        {
            if (upgrade == null) continue;

            upgrade.UnlockUpgrade();
            Debug.Log("Upgrade Unlocked: " + upgrade.name);
        }
    }
}