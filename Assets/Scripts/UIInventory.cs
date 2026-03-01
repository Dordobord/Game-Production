using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventory : MonoBehaviour
{
    [SerializeField]private Image[] slots;
    [SerializeField]private Sprite emptySlot;
    [SerializeField]private ItemIconDataBase iconDataBase;

    private PlayerInventory inventory;

    private void OnEnable()
    {
        StartCoroutine(WaitForInventory());
    }

    private IEnumerator WaitForInventory()
    {
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
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.inventory.Count)
            {
                ItemType item = inventory.inventory[i];
                slots[i].sprite = iconDataBase.GetIcon(item);
                slots[i].enabled = true;
            }
            else
            {
                slots[i].sprite = emptySlot;
                slots[i].enabled = true;
            }
        }
    }
}
