using UnityEngine;

public class Pantry : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemType itemToGive;

    [System.Obsolete]
    public void Interact()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            bool added = playerInventory.AddItem(itemToGive);
            if (added)
            {
                Debug.Log("Added " + itemToGive + " to inventory.");
            }
            else
            {
                Debug.Log("Inventory full! Cannot add " + itemToGive);
            }
        }
    }
}
