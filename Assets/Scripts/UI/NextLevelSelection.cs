using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextLevelSelection : MonoBehaviour
{
    [Header("Level1 UI")]
    public GameObject level1Panel;

    [Header("Level2 UI")]
    public GameObject level2Panel;

    [Header("Level3 UI")]
    public GameObject level3Panel;

    [Header("Level4 UI")]
    public GameObject level4Panel;

    [Header("Level5 UI")]
    public GameObject level5Panel;

    void Start()
    {
        level1Panel.SetActive(true);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        level4Panel.SetActive(false);
        level5Panel.SetActive(false);
    }
    
    public void Level1NextLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(true);
        level3Panel.SetActive(false);
        level4Panel.SetActive(false);
        level5Panel.SetActive(false);
    }

    public void Level2BackLevel()
    {
        level1Panel.SetActive(true);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        level4Panel.SetActive(false);
        level5Panel.SetActive(false);
    }

    public void Level2NextLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(true);
        level4Panel.SetActive(false);
        level5Panel.SetActive(false);
    }

    public void Level3BackLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(true);
        level3Panel.SetActive(false);
        level4Panel.SetActive(false);
        level5Panel.SetActive(false);
    }

    public void Level3NextLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        level4Panel.SetActive(true);
        level5Panel.SetActive(false);
    }

    public void Level4BackLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(true);
        level4Panel.SetActive(false);
        level5Panel.SetActive(false);
    }

    public void Level4NextLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        level4Panel.SetActive(false);
        level5Panel.SetActive(true);
    }

    public void Level5BackLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        level4Panel.SetActive(true);
        level5Panel.SetActive(false);
    }


}
