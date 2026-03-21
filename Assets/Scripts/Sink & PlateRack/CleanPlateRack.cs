using UnityEngine;

public class CleanPlateRack : MonoBehaviour, IInteractable
{
    public static CleanPlateRack main { get; private set; }
    [SerializeField] private int cleanPlatesCount = 0;
    
    [SerializeField] private SpriteRenderer rackSpriteRenderer;
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite emptySprite;

    public int GetCount() => cleanPlatesCount;

    void Awake()
    {
        main = this;
        UpdateVisuals();
    }

    public void IncreasePlate()
    {
        cleanPlatesCount++;
        UpdateVisuals();
    }

    public void Interact()
    {
        PlayerInventory player = PlayerInventory.main;

        if (player == null) return;

        if (player.HasItem(ItemType.Plate)) // player has clean plate on their inventory
        {
            player.RemoveItem(ItemType.Plate); // return to rack
            cleanPlatesCount++;
        }
        else // player does not have any clean plate
        {
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
        
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if(cleanPlatesCount < 1) // empty plate rack
        {
            rackSpriteRenderer.sprite = emptySprite;
        }
        else // have at least 1 plate
        {
            rackSpriteRenderer.sprite = fullSprite;
        }
    }
}
