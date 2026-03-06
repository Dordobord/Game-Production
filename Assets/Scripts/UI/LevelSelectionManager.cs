using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectionManager : MonoBehaviour
{   
    // Level 1
    [Header("Level1")]
    public GameObject level1Panel;
    public GameObject level1SaveSlots;
    public Button level1ContinueButton;
    public Button level1LoadButton;
    public TextMeshProUGUI level1LastSaveText;

    [Header("Level1 Slots")]
    public int[] level1SlotDays = {0, 0, 0, 0, 0};
    public int[] level1SlotWallet = {0, 0, 0, 0, 0};

    private int currentSelectedSlotlvl1 = -1;

    // Level 2
    [Header("Level2")]
    public GameObject level2Panel;
    public GameObject level2SaveSlots;
    public Button level2ContinueButton;
    public Button level2LoadButton;
    public TextMeshProUGUI level2LastSaveText;

    [Header("Level2 Slots")]
    public int[] level2SlotDays = {0, 0, 0, 0, 0};
    public int[] level2SlotWallet = {0, 0, 0, 0, 0};

    private int currentSelectedSlotlvl2 = -1;

    void Start()
    {
        level1SaveSlots.SetActive(false);
        level2SaveSlots.SetActive(false);

        level2Panel.SetActive(false);
    }

    //Level 1 Methods
    public void SelectSaveSlotLevel1(int slotindexlvl1)
    {
        int level1selectedDay = level1SlotDays[slotindexlvl1];

        if(level1selectedDay >= 1 && level1selectedDay <= 5)
        {
            currentSelectedSlotlvl1 = slotindexlvl1;
            Debug.Log("Selected Save Slot Day: " + (slotindexlvl1 + 1)  + level1SlotDays[slotindexlvl1] + " | Wallet: " + level1SlotWallet[slotindexlvl1]);
            level1LastSaveText.text = "Day: " + level1SlotDays[slotindexlvl1] + " | Wallet: " + level1SlotWallet[slotindexlvl1];
        }
        else
        {
            Debug.Log("Selected Save Slot is empty. Please select a save slot with a saved game.");
            level1LastSaveText.text = "Empty Slot";
        }
    }

    public void ContinueLevel1()
    {
        if(currentSelectedSlotlvl1 != -1 && level1LastSaveText.text != "Empty Slot")
        {
            Debug.Log("Continuing from Save Slot " + (currentSelectedSlotlvl1 + 1) + "Day: " + level1SlotDays[currentSelectedSlotlvl1] + " | Wallet: " + level1SlotWallet[currentSelectedSlotlvl1]);
            //SceneManager.LoadScene("Level1");
        }
        else
        {
            Debug.Log("No save slot selected. Please select a save slot before continuing.");
        }
    }

    public void LoadLevel1()
    {
        level1SaveSlots.SetActive(true);

        level1ContinueButton.interactable = false;
        level1LoadButton.interactable = false;
    }

    public void BackToLevel1()
    {
        level1SaveSlots.SetActive(false);

        level1ContinueButton.interactable = true;
        level1LoadButton.interactable = true;
    }

    public void Level1NextLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(true);
    }

    //Level 2 Methods
    public void SelectSaveSlotLevel2(int slotindexlvl2)
    {
        int level2selectedDay = level2SlotDays[slotindexlvl2];

        if(level2selectedDay >= 6 && level2selectedDay <= 10)
        {
            currentSelectedSlotlvl2 = slotindexlvl2;
            Debug.Log("Selected Save Slot Day: " + (slotindexlvl2 + 1)  + level2SlotDays[slotindexlvl2] + " | Wallet: " + level2SlotWallet[slotindexlvl2]);
            level2LastSaveText.text = "Day: " + level2SlotDays[slotindexlvl2] + " | Wallet: " + level2SlotWallet[slotindexlvl2];
        }
        else
        {
            Debug.Log("Selected Save Slot is empty. Please select a save slot with a saved game.");
            level2LastSaveText.text = "Empty Slot";
        }
    }

    public void ContinueLevel2()
    {
        if(currentSelectedSlotlvl2 != -1 && level2LastSaveText.text != "Empty Slot")
        {
            Debug.Log("Continuing from Save Slot " + (currentSelectedSlotlvl2 + 1) + " Day: " + level2SlotDays[currentSelectedSlotlvl2] + " | Wallet: " + level2SlotWallet[currentSelectedSlotlvl2]);
            //SceneManager.LoadScene("Level2");
        }
        else
        {
            Debug.Log("No save slot selected. Please select a save slot before continuing.");
        }
    }

    public void LoadLevel2()
    {
        level2SaveSlots.SetActive(true);

        level2ContinueButton.interactable = false;
        level2LoadButton.interactable = false;
    }

    public void BackToLevel2()
    {
        level2SaveSlots.SetActive(false);

        level2ContinueButton.interactable = true;
        level2LoadButton.interactable = true;
    }

    public void Level2NextLevel()
    {
        level2Panel.SetActive(false);
        //level3Panel.SetActive(true);
    }

    public void Level2PreviousLevel()
    {
        level2Panel.SetActive(false);
        level1Panel.SetActive(true);
    }

}
