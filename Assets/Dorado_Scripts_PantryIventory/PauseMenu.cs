using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool isOpen = false;

    void Start()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIPantryInventory.main != null && UIPantryInventory.main.IsOpen)
            {
                return;
            }
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isOpen = !isOpen;
        panel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0f : 1f;
    }

    public void Resume()
    {
        isOpen = false;
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartDay()
    {
        Time.timeScale = 1f;
        LevelManager.main.RestartDay();
    }

    public void GoToLevelSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection"); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}