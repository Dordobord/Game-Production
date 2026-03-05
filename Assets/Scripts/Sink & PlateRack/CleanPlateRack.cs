using UnityEngine;

public class CleanPlateRack : MonoBehaviour, IInteractable
{
    public static CleanPlateRack main { get; private set; }
    [SerializeField] private int cleanPlatesCount = 0;

    public int GetCount() => cleanPlatesCount;

    void Awake()
    {
        main = this;
    }

    public void IncreasePlate()
    {
        cleanPlatesCount++;
    }

    public void Interact()
    {
        PlayerInventory player = PlayerInventory.main;

        if (player == null) return;

        if (cleanPlatesCount > 0)
        {
            if (player.AddItem(ItemType.Plate))
                cleanPlatesCount--;
            else
                Debug.Log("Inventory full.");
        }
        else
        {
            Debug.Log("No plates available.");
        }
    }
}
