using UnityEngine;
using System.Collections.Generic;

public class LevelSelection : MonoBehaviour
{
    [System.Serializable]
    public struct LevelData
    {
        public string levelName;
        public int levelID;
        public int startingDay;
        public int maxDays;
    }
    public LevelData[] levels;

    public void StartGame(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            LevelData selected = levels[levelIndex];

            LevelManager.main.InitializeLevel(selected.levelID, selected.startingDay, selected.maxDays);
            
            Debug.Log($"Level Selection Manager: Starting {selected.levelName}");
        }
    }
}