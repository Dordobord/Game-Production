using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    public GameObject settingsUIPanel;
    public GameObject creditsUIPanel;

    [Header("Store")]
    public GameObject storeUIPanel;
    public GameObject inStoreUIPanel;
    public GameObject bcUIPanel;

    void Awake()
    {
        settingsUIPanel.SetActive(false);
        creditsUIPanel.SetActive(false);

        storeUIPanel.SetActive(false);
        bcUIPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    //SettingsUI
    public void SettingsPanel()
    {
        settingsUIPanel.SetActive(true);
    }

    public void CreditsPanel()
    {
        creditsUIPanel.SetActive(true);
    }

    public void SettingsPanelClose()
    {
        settingsUIPanel.SetActive(false);
    }

    public void CreditsPanelClose()
    {
        creditsUIPanel.SetActive(false);
    }

    //StoreUI
    public void StorePanel()
    {
        storeUIPanel.SetActive(true);
    }

    public void StorePanelClose()
    {
        storeUIPanel.SetActive(false);
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
