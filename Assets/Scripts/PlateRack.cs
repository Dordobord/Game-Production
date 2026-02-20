using UnityEngine;

public class PlateRack : MonoBehaviour, IInteractable
{
    [SerializeField] private int plateCount = 5; // Starting number of plates

    private PlayerInventory playerInventory;

    [System.Obsolete]
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void Interact()
    {
        if (playerInventory == null)
            return;

        if (plateCount <= 0)
        {
            Debug.Log("No clean plates available!");
            return;
        }

        bool added = playerInventory.AddItem(ItemType.Plate);

        if (added)
        {
            plateCount--;
            Debug.Log("Took a plate. Plates remaining: " + plateCount);
        }
        else
        {
            Debug.Log("Inventory full! Cannot take plate.");
        }
    }

    public void AddPlate()
    {
        plateCount++;
        Debug.Log("Plate added to rack. Total plates: " + plateCount);
    }
}
