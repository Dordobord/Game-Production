using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static LevelData selectedLevel;

    public void StartGame(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            selectedLevel = levels[levelIndex]; 

            Debug.Log($"Starting {selectedLevel.levelName}");

            SceneManager.LoadScene("GameScene");
            // LevelData selected = levels[levelIndex];

            // LevelManager.main.InitializeLevel(selected.levelID, selected.startingDay, selected.maxDays);
            
            // Debug.Log($"Level Selection Manager: Starting {selected.levelName}");
        }

        
    }
}