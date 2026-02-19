using UnityEngine;
using TMPro;
using System.Collections;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slotTexts;

    private PlayerInventory inventory;

    private void OnEnable()
    {
        StartCoroutine(WaitForInventory());
    }

    private IEnumerator WaitForInventory()
    {
        // Wait until PlayerInventory exists
        while (PlayerInventory.main == null)
            yield return null;

        inventory = PlayerInventory.main;
        inventory.OnInventoryChanged += UpdateInventoryUI;

        UpdateInventoryUI();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= UpdateInventoryUI;
    }

    private void UpdateInventoryUI()
    {
        if (slotTexts == null || slotTexts.Length == 0)
            return;

        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (slotTexts[i] == null)
                continue;

            if (i < inventory.inventory.Count)
                slotTexts[i].text = inventory.inventory[i].ToString();
            else
                slotTexts[i].text = "[  ]";
        }
    }
}
