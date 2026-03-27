using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject restaurantImage;

    [Header("Settings")]
    public GameObject settingsUIPanel;
    public GameObject creditsUIPanel;

    [Header("Store")]
    public GameObject storeUIPanel;
    public GameObject inStoreUIPanel;
    public GameObject bcUIPanel;

    void Awake()
    {
        restaurantImage.SetActive(true);
        settingsUIPanel.SetActive(false);
        creditsUIPanel.SetActive(false);

        storeUIPanel.SetActive(false);
        bcUIPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    //SettingsUI
    public void SettingsPanel()
    {
        settingsUIPanel.SetActive(true);
        restaurantImage.SetActive(false);
    }

    public void CreditsPanel()
    {
        creditsUIPanel.SetActive(true);
    }

    public void SettingsPanelClose()
    {
        settingsUIPanel.SetActive(false);
        restaurantImage.SetActive(true);
    }

    public void CreditsPanelClose()
    {
        creditsUIPanel.SetActive(false);
    }

    //StoreUI
    public void StorePanel()
    {
        storeUIPanel.SetActive(true);
        restaurantImage.SetActive(false);
    }

    public void StorePanelClose()
    {
        storeUIPanel.SetActive(false);
        restaurantImage.SetActive(true);
    }

    public void BCPanel()
    {
        bcUIPanel.SetActive(true);
        inStoreUIPanel.SetActive(false);
    }

    public void BCPanelClose()
    {
        bcUIPanel.SetActive(false);
        inStoreUIPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
