using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level3Manager : MonoBehaviour
{
    public GameObject level1Panel;
    public GameObject level2Panel;
    public GameObject level3Panel;

    public GameObject level1SaveSlots;
    public GameObject level2SaveSlots;
    public GameObject level3SaveSlots;

    public Button level3ContinueButton;
    public Button level3LoadButton;
    public TextMeshProUGUI level3LastSaveText;

    public int[] level3SlotDays = { 0, 0, 0, 0, 0 };
    public int[] level3SlotWallet = { 0, 0, 0, 0, 0 };

    private int currentSelectedSlotlvl3 = -1;

    void Start()
    {
        level1SaveSlots.SetActive(false);
        level2SaveSlots.SetActive(false);
        level3SaveSlots.SetActive(false);

        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(true);
    }

    public void SelectSaveSlotLevel3(int slotindexlvl3)
    {
        int level3selectedDay = level3SlotDays[slotindexlvl3];

        if (level3selectedDay >= 11 && level3selectedDay <= 15)
        {
            currentSelectedSlotlvl3 = slotindexlvl3;
            Debug.Log("Selected Save Slot Day: " + (slotindexlvl3 + 1) + level3SlotDays[slotindexlvl3] + " | Wallet: " + level3SlotWallet[slotindexlvl3]);
            level3LastSaveText.text = "Day: " + level3SlotDays[slotindexlvl3] + " | Wallet: " + level3SlotWallet[slotindexlvl3];
        }
        else
        {
            Debug.Log("Selected Save Slot is empty. Please select a save slot with a saved game.");
            level3LastSaveText.text = "Empty Slot";
        }
    }

    public void ContinueLevel3()
    {
        if (currentSelectedSlotlvl3 != -1 && level3LastSaveText.text != "Empty Slot")
        {
            Debug.Log("Continuing from Save Slot " + (currentSelectedSlotlvl3 + 1) + " Day: " + level3SlotDays[currentSelectedSlotlvl3] + " | Wallet: " + level3SlotWallet[currentSelectedSlotlvl3]);
            //SceneManager.LoadScene("Level3");
        }
        else
        {
            Debug.Log("No save slot selected. Please select a save slot before continuing.");
        }
    }

    public void LoadLevel3()
    {
        level3SaveSlots.SetActive(true);

        level3ContinueButton.interactable = false;
        level3LoadButton.interactable = false;
    }

    public void BackToLevel3()
    {
        level3SaveSlots.SetActive(false);

        level3ContinueButton.interactable = true;
        level3LoadButton.interactable = true;
    }

    public void Level3NextLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        //level4Panel.SetActive(true);
    }

    public void Level3PreviousLevel()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(true);
        level3Panel.SetActive(false);
    }

}
